using Microsoft.AspNetCore.Hosting.Server.Features;

namespace GameServicesCloud.Accounts.StartupSetup;

public static class StartupSetupExtensions {
    public static async Task Setup(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();

        var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

        await ActivatorUtilities.CreateInstance<ClaimSetup>(scope.ServiceProvider).Run();

        lifetime.ApplicationStarted.Register(async () => {
            await PostStartupSetup(app);
        });
    }

    private static async Task PostStartupSetup(IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();

        await ActivatorUtilities.CreateInstance<RootAdminSetup>(scope.ServiceProvider).Run(app.ServerFeatures.Get<IServerAddressesFeature>()!);
    }
}