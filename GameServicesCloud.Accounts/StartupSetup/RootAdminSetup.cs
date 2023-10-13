using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Accounts.StartupSetup;

public class RootAdminSetup {
    private readonly ILogger<RootAdminSetup> _logger;
    private readonly IUserService _userService;
    private readonly IUserClaimService _userClaimService;
    private readonly RootAdminOptions _options;
    private readonly LinkGenerator _linkGenerator;

    public RootAdminSetup(ILogger<RootAdminSetup> logger,
        IOptions<RootAdminOptions> options,
        IUserService userService,
        LinkGenerator linkGenerator,
        IUserClaimService userClaimService) {
        _logger = logger;
        _userService = userService;
        _linkGenerator = linkGenerator;
        _userClaimService = userClaimService;
        _options = options.Value;
    }

    public async Task Run(IServerAddressesFeature addresses) {
        var user = await _userService.Find(_options.Email, true) ?? await CreateAdmin();

        if (!user.IsActivated) {
            ActivateAdmin(user, addresses);
        } else {
            await UpdateAdminClaims(user);
        }
    }

    private Task<User> CreateAdmin() {
        return _userService.RegisterUser(new User { Email = _options.Email })!;
    }

    private void ActivateAdmin(User user, IServerAddressesFeature addresses) {
        _userService.SendActivationEmail(user,
            (email, activationCode) => {
                var address = new Uri(addresses.Addresses.First());

                return _linkGenerator.GetUriByAction("Activate",
                    "Registration",
                    new { email, activationCode },
                    address.Scheme,
                    new HostString(address.Host, address.Port))!;
            });

        _logger.LogWarning("Root admin user created. IT NEEDS EMAIL VERIFICATION, check the email {Email}", _options.Email);
    }

    private async Task UpdateAdminClaims(User user) {
        var userClaims = _userClaimService.GetClaims(user);
        var newClaims = (await _userClaimService.GetAllClaims()).Where(x => userClaims.All(y => y.Name != x.Name));

        foreach (var claim in newClaims) {
            await _userClaimService.AddClaim(claim, user);

            _logger.LogInformation("Claim {Claim} added to root admin", claim.Name);
        }
    }
}