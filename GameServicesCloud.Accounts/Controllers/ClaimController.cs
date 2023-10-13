using GameServicesCloud.Accounts.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClaimController : ControllerBase {
    private readonly IClaimService _claimService;

    public ClaimController(IClaimService claimService) {
        _claimService = claimService;
    }

    [HttpGet("{claimId:long}")]
    [Authorize(Claims.Account.Claim.Read)]
    public async Task<ActionResult<AccountClaimDto>> Find(long claimId) {
        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        return claim.ToDto();
    }

    [HttpGet]
    [Authorize(Claims.Account.Claim.Read)]
    public async Task<IEnumerable<AccountClaimDto>> FindAll() {
        var claims = await _claimService.FindAll();

        return claims.ToDto();
    }
}