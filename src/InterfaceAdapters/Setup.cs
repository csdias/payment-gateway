using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationBusinessRules;
using Microsoft.AspNetCore.Http;

namespace InterfaceAdapters
{
    public static class Setup
    {
        public static IServiceCollection AddInterfaceAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplicationBusinessRules(configuration)
                .AddHttpContextAccessor();

            return services;
        }        

        public static IApplicationBuilder UseInterfaceAdapters(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {            
            app.UseApplicationBusinessRules(env, configuration);

            return app;
        }
    }
}