using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationBusinessRules.Interfaces;
using FrameworksAndDrivers.Database.Contexts;
using FrameworksAndDrivers.Database.Repositories;
using Microsoft.Extensions.Hosting;

namespace FrameworksAndDrivers.Database
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IPaymentRepository, PaymentRepository>()
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(configuration["DATABASE_CONNECTION_STRING"])
                );
            return services;
        }

        public static IApplicationBuilder UseFrameworksAndDriversDatabase(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app;
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {            
        }
    }
}
