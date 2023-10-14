﻿using GameServicesCloud.Accounts.Mapping;
using GameServicesCloud.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ClaimController : ControllerBase {
    private readonly IPaginator<AccountClaim> _paginator;
    private readonly IClaimService _claimService;

    public ClaimController(IClaimService claimService, IPaginator<AccountClaim> paginator) {
        _claimService = claimService;
        _paginator = paginator;
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
    public async Task<IEnumerable<AccountClaimDto>> FindAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string filter = "") {
        return (await _paginator.List(page, pageSize, x => x.Name.Contains(filter))).ToDto();
    }

    [HttpGet("count")]
    [Authorize(Claims.Account.Claim.Read)]
    public async Task<int> Count([FromQuery] string filter = "") {
        return await _paginator.Count(x => x.Name.Contains(filter));
    }
}