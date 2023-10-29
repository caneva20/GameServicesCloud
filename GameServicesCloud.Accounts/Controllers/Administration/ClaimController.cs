using System.Linq.Expressions;
using GameServicesCloud.Accounts.Mapping;
using GameServicesCloud.Controllers;
using GameServicesCloud.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers.Administration;

[ApiController]
[Route("admin/[controller]")]
public class ClaimController : PaginationController<AccountClaim, AccountClaimDto> {
    private readonly IClaimService _claimService;

    public ClaimController(IClaimService claimService, IPaginator<AccountClaim> paginator) : base(paginator) {
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

    [HttpPut("{claimId:long}")]
    public async Task<IActionResult> SetDefault(long claimId, [FromBody] bool isDefault) {
        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        await _claimService.SetDefault(claim, isDefault);

        return Ok();
    }

    protected override Expression<Func<AccountClaim, bool>> Filter(string filter) {
        return x => x.Name.Contains(filter);
    }

    protected override AccountClaimDto ConvertToDto(AccountClaim entity) {
        return entity.ToDto();
    }
}