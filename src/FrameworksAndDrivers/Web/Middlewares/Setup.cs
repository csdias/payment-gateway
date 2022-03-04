using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworksAndDrivers.Web.Middlewares
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebMiddlewares(this IServiceCollection services, IConfiguration configuration)
        {             
            return services;
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebMiddlewares(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}
