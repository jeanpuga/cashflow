using APPLICATION.Features.Auth.Repository;
using APPLICATION.Features.Auth.Service;
using APPLICATION.Features.Operations.Broker;
using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Repository;
using APPLICATION.Shared.Domain.Interfaces;
using APPLICATION.Shared.Domain.Options;
using APPLICATION.Shared.ExternalServices;
using APPLICATION.Shared.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace API.Customize
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder builder) where TStartup : IStartup
        {
            if (Activator.CreateInstance(typeof(TStartup), builder.Configuration) is not IStartup startup) throw new ArgumentException("");

            startup.ConfigureServices(builder.Services);

            var logSettings = new LogOptions();
            builder.Configuration.GetSection("LogOptions").Bind(logSettings);
            builder.Services.Configure<LogOptions>(builder.Configuration.GetSection("LogOptions"));

            var loggingOperationsFeatureOn = bool.Parse(builder.Configuration.GetSection("FeatureFlags").GetSection("LoggingOperationsFeatureOn").Value);

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn> {
                        new SqlColumn { ColumnName = "Action" }
                    }
            };

            if (loggingOperationsFeatureOn)
            {
                var logger = Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .WriteTo.Seq(logSettings.Url)
                 .WriteTo.MSSqlServer(
                     connectionString: logSettings.Database,
                     tableName: logSettings.Tablename,
                     columnOptions: columnOptions,
                     appConfiguration: builder.Configuration
                 )
                 .CreateLogger();

                builder.Host.UseSerilog(logger);
            }
            else
            {
                var logger = Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                 .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .CreateLogger();

                builder.Host.UseSerilog(logger);
            }

            var app = builder.Build();
            startup.Configure(app, app.Environment);
            app.Run();
            return builder;
        }

        internal static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var urls = configuration.GetSection("APIs")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var url in urls)
                services.AddHttpClient(url.Key, c => c.BaseAddress = new System.Uri(url.Value));
        }

        internal static void AddConfigurations(this IServiceCollection services)
        {
            services.AddSingleton<ISecretKey, SecretKey>();
        }

        internal static void AddCORs(this IServiceCollection services, IConfiguration configuration)
        {
            var BASE_URL_FRONTEND = configuration.GetSection("BASE_URL_FRONTEND").Value;
            string[] origins = new[] { BASE_URL_FRONTEND };

            services.AddCors(options => options.AddPolicy(name: "__allowOrigins",
                 policy => policy.WithOrigins(origins)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod())
                );
        }

        internal static void AddHealth(this IServiceCollection services, IConfiguration configuration)
        {
            var healthSettings = new HealthcheckOptions();
            configuration.GetSection("HealthcheckOptions").Bind(healthSettings);

            services.AddHealthChecks();

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);
                options.MaximumHistoryEntriesPerEndpoint(60);
                options.SetApiMaxActiveRequests(1);

                options.AddHealthCheckEndpoint("SQLSERVER", $"{healthSettings.Url}/Health/sqlserver");
                options.AddHealthCheckEndpoint("SQLSERVER_LOGS", $"{healthSettings.Url}/Health/sqlserverlogs");
                options.AddHealthCheckEndpoint("SEQLOG", $"{healthSettings.Url}/Health/seqlog");
                options.AddHealthCheckEndpoint("RABBITMQ", $"{healthSettings.Url}/Health/rabbitmq");
            })
            .AddInMemoryStorage();
        }

        internal static void AddMessageQueue(this IServiceCollection services, IConfiguration configuration)
        {
            var featureConsumerOperationsOn = bool.Parse(configuration.GetSection("FeatureFlags")
                                                                      .GetSection("ConsumeOperationsFeatureOn").Value);

            var _rabbitMqSettings = new RabbitmqOptions();
            configuration.GetSection("RabbitmqOptions").Bind(_rabbitMqSettings);
            services.Configure<RabbitmqOptions>(configuration.GetSection("RabbitmqOptions"));

            services.AddMassTransit(mass =>
            {
                if (featureConsumerOperationsOn)
                    mass.AddConsumer<ConsumerCreateOperation>();

                mass.UsingRabbitMq((context, config) =>
                {
                    config.Host(_rabbitMqSettings.Host, _rabbitMqSettings.VirtualHost, h =>
                    {
                        h.Username(_rabbitMqSettings.User);
                        h.Password(_rabbitMqSettings.Password);
                    });

                    config.Publish<SubmitOperationMessage>(x =>
                    {
                        x.Durable = true;
                        x.AutoDelete = false;
                        x.ExchangeType = "fanout";
                    });

                    config.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(7)));

                    config.UseMessageRetry(r => r.Immediate(5));

                    config.ConfigureEndpoints(context);
                });
            });
        }

        internal static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnString = configuration.GetConnectionString("Database");
            services.AddScoped<IDbConnection>(_ => new SqlConnection(ConnString));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IOperationsRepository, OperationsRepository>();
            services.AddScoped<IHealthRepository, HealthRepository>();

            //deletar
            services.AddTransient<IConn>(_ => new Conn(ConnString));
        }

        internal static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IRabbitmqHealthServices, RabbitmqHealthServices>();
            services.AddTransient<ISeqlogHealthServices, SeqlogHealthServices>();
        }

        internal static void AddSwaggerCustomize(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Operations",
                    Version = "v1.0.0",
                    Description = "API to support platform services and data control Cashflow."
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement { {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                option.SchemaFilter<SwaggerExtensions>();
            });
        }

        public interface IConn
        {
            string ConnectionString { get; set; }
        }

        public class Conn : IConn
        {
            public Conn(string connectionString)
            {
                ConnectionString = connectionString;
            }

            public string ConnectionString { get; set; }
        }
    }
}