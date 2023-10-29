using GameServicesCloud.Data;
using GameServicesCloud.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud;

public static class SharedModuleExtensions {
    public static void AddSharedServices(this IServiceCollection services) {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IPaginator<>), typeof(Paginator<>));

        services.AddSingleton<IControllerClaimProviderService, ControllerClaimProviderService>();
    }

    public static void AddMailServices(this IServiceCollection services, IConfiguration mailOptions, IConfiguration templateOptions) {
        services.Configure<MailOptions>(mailOptions);
        services.Configure<MailTemplateOptions>(templateOptions);

        services.AddSingleton<IMailTemplateService, MailTemplateService>();
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<ITokenService, TokenService>();
    }

    public static void UseSharedServices(this IApplicationBuilder app, Action<SharedServicesOptions> optionsBuilder) {
        using var scope = app.ApplicationServices.CreateScope();

        var options = new SharedServicesOptions();
        optionsBuilder(options);

        scope.ServiceProvider.GetRequiredService<IControllerClaimProviderService>().Initialize(options.ClaimPrefix);
    }

    public class SharedServicesOptions {
        public string ClaimPrefix { get; set; } = null!;
    }
}