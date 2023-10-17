using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace API.Customize
{
    public interface IStartup
    {
        void Configure(WebApplication app, IWebHostEnvironment env);

        void ConfigureServices(IServiceCollection services);
    }
}