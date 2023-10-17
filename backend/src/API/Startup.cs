using API.Customize;
using API.Filter;
using APPLICATION.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace API
{
    public class Startup : Customize.IStartup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCORs(_configuration);

            services.AddConfigurations();

            var provider = services.BuildServiceProvider();

            var _secretKey = provider.GetService<ISecretKey>();

            var key = Encoding.ASCII.GetBytes(_secretKey.Key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddMemoryCache();

            services.AddMessageQueue(_configuration);

            var assembly = AppDomain.CurrentDomain.Load("APPLICATION");

            services.AddMediatR(assembly);

            services.AddRepositories(_configuration);

            services.AddServices();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AuthorizeAttribute>();
            });

            services.AddHealth(_configuration);

            services.AddApiServices(_configuration);

            services.AddSwaggerCustomize(_configuration);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                RequestPath = "/Content"
            });

            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1.0.0");
                    c.InjectStylesheet("/Content/css/swagger.css");
                    c.InjectJavascript("Content/js/swagger.js");
                });

                app.UseHsts();
            }

            app.UseCors("__allowOrigins");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHealthChecksUI(options =>
            {
                options.AddCustomStylesheet("Content/css/healthui.css");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions());

                endpoints.MapHealthChecksUI();

                endpoints.MapControllers();
            });
        }
    }
}