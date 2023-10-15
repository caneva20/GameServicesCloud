using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts.StartupSetup;

public class ClaimSetup {
    private readonly ILogger<ClaimSetup> _logger;
    private readonly IRepository<AccountClaim> _repository;
    private readonly IControllerClaimProviderService _controllerClaimProviderService;

    public ClaimSetup(ILogger<ClaimSetup> logger, IRepository<AccountClaim> repository, IControllerClaimProviderService controllerClaimProviderService) {
        _logger = logger;
        _repository = repository;
        _controllerClaimProviderService = controllerClaimProviderService;
    }

    public async Task Run() {
        _logger.LogInformation("Updating claim register");

        var registeredClaims = await _repository.FindAll();

        var claims = _controllerClaimProviderService.Claims.ToList();
        var newClaims = claims.Except(registeredClaims.Select(x => x.Name)).ToList();
        var oldClaims = registeredClaims.Where(x => !claims.Contains(x.Name)).ToList();

        _logger.LogInformation("Creating {Quantity} new claims: {@Claims}", newClaims.Count, newClaims);
        _logger.LogInformation("Removing {Quantity} old claims: {@Claims}", oldClaims.Count, oldClaims.Select(x => x.Name));

        try {
            await _repository.SaveAll(newClaims.Select(x => new AccountClaim { Name = x }));
            await _repository.RemoveAll(oldClaims);
        } catch (Exception e) {
            _logger.LogCritical(e, "Failed to update claim register!!");
        }
    }
}