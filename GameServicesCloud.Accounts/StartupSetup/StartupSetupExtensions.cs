namespace GameServicesCloud.Accounts.StartupSetup;

public static class StartupSetupExtensions {
    public static async Task SetupClaims(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();

        await ActivatorUtilities.CreateInstance<ClaimSetup>(scope.ServiceProvider).Run();
    }
}