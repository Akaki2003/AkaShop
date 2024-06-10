using AkaShop.Auth;
using AkaShop.Domain.Entities;
using AkaShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AkaShop.Infrastructure.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value;
            var keybytes = Encoding.ASCII.GetBytes(key);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keybytes),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                });

            return services;
        }
    }
}
