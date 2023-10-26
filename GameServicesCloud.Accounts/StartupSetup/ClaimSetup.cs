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

        var claims = _controllerClaimProviderService.Claims;
        var newClaims = claims.Except(registeredClaims.Select(x => x.Name)).ToList();

        _logger.LogInformation("Creating {Quantity} new claims: {@Claims}", newClaims.Count, newClaims);

        try {
            await _repository.SaveAll(newClaims.Select(x => new AccountClaim { Name = x }));
        } catch (Exception e) {
            _logger.LogCritical(e, "Failed to update claim register!!");
        }
    }
}