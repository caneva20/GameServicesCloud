using GameServicesCloud.Abstractions.Mail;
using GameServicesCloud.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameServicesCloud; 

public static class SharedModuleExtensions {
    public static void AddSharedServices(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<MailOptions>(configuration);
        
        services.AddSingleton<IMailService, MailService>();
    }
}