using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworksAndDrivers.Web.Cors
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebCors(this IServiceCollection services, IConfiguration configuration)
        {           
            services
                .AddCors();   
            return services;
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebCors(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseCors(builder => builder.AllowAnyMethod()
                                          .AllowAnyOrigin()
                                          .AllowAnyHeader()
                                          //.AllowCredentials()
                                          );
            return app;
        }
    }
}
