using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text.Json;
using FrameworksAndDrivers.Database.Contexts;

namespace FrameworksAndDrivers.Web.Checks
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebChecks(this IServiceCollection services, IConfiguration configuration)
        {           
            services
                .AddHealthChecks()
                .AddRedis(configuration["REDIS_CONNECTION_STRING"], "redis")
                .AddNpgSql(configuration["DATABASE_CONNECTION_STRING"], "select now()")
                .AddDbContextCheck<ApplicationDbContext>(name: "database")
                .AddProcessAllocatedMemoryHealthCheck(1024)
                .AddWorkingSetHealthCheck(3000_000_000L)
                .AddProcessHealthCheck("dotnet", process =>
                {
                    var proc = process.FirstOrDefault();
                    return !proc.HasExited && proc.Responding;
                });
            return services;
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebChecks(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks(configuration["HEALTHCHECK_ROUTE"], new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = async (context, healthReport) =>
                    {
                        context.Response.ContentType = "application/json";
                        var result = JsonSerializer.Serialize(new
                        {
                            check = string.Join("|", healthReport.Entries.Select(e => e.Key).ToList()),
                            status = healthReport.Status,
                            description = healthReport.Status.ToString(),
                            dutation = healthReport.TotalDuration.ToString(),    
                            detail = healthReport.Entries.Select(e => new
                            {
                                check = e.Key,                          
                                status = e.Value.Status,
                                description = e.Value.Status.ToString(),
                                dutation = e.Value.Duration,
                                exception = e.Value.Exception                                                   
                            })
                        });
                        await context.Response.WriteAsync(result);
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });

            return app;
        }
    }
}
