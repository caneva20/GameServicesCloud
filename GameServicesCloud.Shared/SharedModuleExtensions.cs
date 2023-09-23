using GameServicesCloud.Data;
using GameServicesCloud.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud;

public static class SharedModuleExtensions {
    public static void AddSharedServices(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<MailOptions>(configuration);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<ITokenService, TokenService>();
    }
}