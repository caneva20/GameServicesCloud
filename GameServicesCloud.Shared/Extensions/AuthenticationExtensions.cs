using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GameServicesCloud.Extensions;

public static class AuthenticationExtensions {
    public static void AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration) {
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.IncludeErrorDetails = true;

                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]!)),
                    ValidIssuer = configuration["Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = configuration["Audience"],
                    ValidateAudience = true
                };
            });
    }
}