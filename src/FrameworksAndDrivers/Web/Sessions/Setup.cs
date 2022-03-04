using InterfaceAdapters.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworksAndDrivers.Web.Sessions
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebSessions(this IServiceCollection services, IConfiguration configuration)
        {             
            return services
                .AddScoped<IClientSession, ClientSession>();                
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebSessions(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app;
        }
    }
}
