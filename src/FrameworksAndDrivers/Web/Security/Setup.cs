using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FrameworksAndDrivers.Web.Security
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebSecurity(this IServiceCollection services,
            IConfiguration configuration)
        {     
             var securityKey = Encoding.ASCII.GetBytes(configuration["JWT_TOKEN"]);
             
             services
                .AddAuthentication (builder =>
                {
                    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (builder => {
                    builder.RequireHttpsMetadata = false;
                    builder.SaveToken = true;
                    builder.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey (securityKey),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT_AUDIENCE"],
                        ValidIssuer = configuration["JWT_ISSUER"]
                    };
                });  

            return services;              
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebSecurity(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            return app;
        }
    }
}