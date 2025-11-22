using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SharedService.DI
{
    public static class JWTAuthenticationScheme
    {
        public static IServiceCollection AddJWTAuthScheme(this IServiceCollection services,IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Bearer", options =>
            {
                var key = Encoding.UTF8.GetBytes(config.GetSection("Authentication:Key").Value);
                string issuer = config.GetSection("Authentication:Issuer").Value;
                string audiance = config.GetSection("Authentication:Audiance").Value;

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audiance,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };

            });
            return services;
        }
    }
}
