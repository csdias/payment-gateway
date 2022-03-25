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
using FrameworksAndDrivers.Database.Models;
using Npgsql;

namespace FrameworksAndDrivers.Database
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string hostname = "127.0.0.1";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Lambda Development")
            {
                hostname = Environment.GetEnvironmentVariable("RDS_HOSTNAME");
            }

            var dbConnectionBuilder = new NpgsqlConnectionStringBuilder()
            {
                Host = hostname,
                Port = 5432,
                Username = "postgres",
                Database = "PaymentGateway",
                Password = "postgrespwd",
                MaxPoolSize = 100
            };

            services
                .AddScoped<IPaymentRepository, PaymentRepository>()
                .AddDbContext<ApplicationDbContext>(
                    options => options.UseNpgsql(dbConnectionBuilder.ConnectionString)
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
            modelBuilder.Entity<PaymentStatusModel>().HasData(
                new PaymentStatusModel() { Id = 1, Status = "Received" },
                new PaymentStatusModel() { Id = 2, Status = "Queued" },
                new PaymentStatusModel() { Id = 3, Status = "PaymentCommited" },
                new PaymentStatusModel() { Id = 4, Status = "PaymentRejected" }
            );

            modelBuilder.Entity<CreditStatusModel>().HasData(
                new CreditStatusModel() { Id = 1, Status = "Verified" }
            );

            modelBuilder.Entity<MerchantModel>().HasData(
                new MerchantModel() { Id = 1, BankAccountDetails = "BankAccountDetails" }
            );

            modelBuilder.Entity<CreditCardModel>().HasData(
                new CreditCardModel()
                {
                    Id = 1,
                    Number = "379354508162306",
                    HolderName = "Antônio J. Penteado",
                    HolderAddress = "Heroic St. 195",
                    ExpirationMonth = "12",
                    ExpirationYear = "2025",
                    Cvv = "323",
                    StatusId = 1
                });

            modelBuilder.Entity<PaymentModel>().HasData(
                new PaymentModel
                {
                    Id = Guid.Parse("fc782e65-0117-4c5e-b6d0-afa845effa3e"),
                    MerchantId = 1,
                    CreditCardId = 1,
                    Amount = 15.99m,
                    Currency = "EUR",
                    SaleDescription = "Final soccer match",
                    StatusId = 1
                }
           );
        }
    }
}
