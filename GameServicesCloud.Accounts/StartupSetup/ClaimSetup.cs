using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts.StartupSetup;

public static class ClaimSetup {
    public static async Task SetupClaims(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<object>>();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository<AccountClaim>>();

        logger.LogInformation("Updating claim register");

        var registeredClaims = await repository.FindAll();

        var newClaims = Claims.AllClaims.Except(registeredClaims.Select(x => x.Name)).ToList();
        var oldClaims = registeredClaims.Where(x => !Claims.AllClaims.Contains(x.Name)).ToList();

        logger.LogInformation("Creating {Quantity} new claims: {@Claims}", newClaims.Count, newClaims);
        logger.LogInformation("Removing {Quantity} old claims: {@Claims}", oldClaims.Count, oldClaims.Select(x => x.Name));

        try {
            await repository.SaveAll(newClaims.Select(x => new AccountClaim { Name = x }));
            await repository.RemoveAll(oldClaims);
        } catch (Exception e) {
            logger.LogCritical(e, "Failed to update claim register!!");
        }
    }
}