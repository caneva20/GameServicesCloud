using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GameServicesCloud.Extensions;

public static class SwaggerExtensions {
    public static void AddDefaultSwaggerGen(this IServiceCollection services) {
        services.AddSwaggerGen(options => {
            var securityScheme = new OpenApiSecurityScheme {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            };

            options.AddSecurityDefinition("Bearer", securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme, Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}