using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FrameworksAndDrivers.Web.Cors;
using FrameworksAndDrivers.Web.Swagger;
using FrameworksAndDrivers.Web.Checks;
using FrameworksAndDrivers.Web.Security;
using FrameworksAndDrivers.Web.Sessions;
using FrameworksAndDrivers.Web.Middlewares;

namespace FrameworksAndDrivers.Web
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers();
            services            
                .AddFrameworksAndDriversWebCors(configuration)
                .AddFrameworksAndDriversWebSwagger(configuration)
                .AddFrameworksAndDriversWebChecks(configuration)
                .AddFrameworksAndDriversWebSecurity(configuration)
                .AddFrameworksAndDriversWebSessions(configuration)
                .AddFrameworksAndDriversWebMiddlewares(configuration);
               
            return services;
        }

        public static IApplicationBuilder UseFrameworksAndDriversWeb(this IApplicationBuilder app,
            IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            return app
                .UseFrameworksAndDriversWebSessions(env, configuration)
                .UseFrameworksAndDriversWebMiddlewares(env, configuration)
                .UseFrameworksAndDriversWebCors(env, configuration)
                .UseFrameworksAndDriversWebSecurity(env, configuration)
                .UseFrameworksAndDriversWebSwagger(env, configuration)
                .UseFrameworksAndDriversWebChecks(env, configuration);
               
        }
    }
}
