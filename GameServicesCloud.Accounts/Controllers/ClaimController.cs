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
    public async Task<ActionResult<AccountClaimDto>> Find(long claimId) {
        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        return claim.ToDto();
    }

    [HttpGet]
    public async Task<IEnumerable<AccountClaimDto>> FindAll() {
        var claims = await _claimService.FindAll();

        return claims.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAccountClaimDto claimDto) {
        if (!await _claimService.Create(claimDto.ToEntity())) {
            return BadRequest();
        }

        return Ok();
    }

    [HttpDelete("{claimId:long}")]
    public async Task<ActionResult> Delete(long claimId) {
        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        await _claimService.Remove(claim);

        return Ok();
    }
}