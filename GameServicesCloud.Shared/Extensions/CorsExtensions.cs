using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud.Extensions;

public static class CorsExtensions {
    public static void AddDefaultCors(this WebApplicationBuilder builder) {
        builder.Services.AddCors(options => {
            var corsOptions = builder.Configuration.GetSection("Cors").Get<CorsOptions>()!;

            options.AddPolicy("DefaultCorsPolicy",
                policy => {
                    policy.WithOrigins(corsOptions.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
        });
    }

    public static void UseDefaultCors(this IApplicationBuilder app) {
        app.UseCors("DefaultCorsPolicy");
    }
}