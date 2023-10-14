using GameServicesCloud.Data;
using GameServicesCloud.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud;

public static class SharedModuleExtensions {
    public static void AddSharedServices(this IServiceCollection services, IConfiguration mailOptions, IConfiguration templateOptions) {
        services.Configure<MailOptions>(mailOptions);
        services.Configure<MailTemplateOptions>(templateOptions);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IPaginator<>), typeof(Paginator<>));
        services.AddSingleton<IMailTemplateService, MailTemplateService>();
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<ITokenService, TokenService>();
    }
}