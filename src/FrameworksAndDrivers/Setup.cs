using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InterfaceAdapters;
using FrameworksAndDrivers.Web;
using FrameworksAndDrivers.Mappers;
using FrameworksAndDrivers.Database;

namespace FrameworksAndDrivers
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDrivers(this IServiceCollection services, IConfiguration configuration)
        {
            return services   
                .AddFrameworksAndDriversWeb(configuration)               
                .AddFrameworksAndDriversDatabase(configuration)
                .AddFrameworksAndDriversMappers(configuration)
                .AddInterfaceAdapters(configuration);
        }

        public static IApplicationBuilder UseFrameworksAndDrivers(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app
                .UseFrameworksAndDriversWeb(env, configuration)
                .UseFrameworksAndDriversDatabase(env, configuration)
                .UseInterfaceAdapters(env, configuration)
                .UseInterfaceAdapters(env, configuration);
        }
    }
}
