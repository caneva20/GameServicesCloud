﻿using GameServicesCloud.Accounts.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers.Administration;

[ApiController]
[Route("admin/[controller]/{userId:long}")]
public class UserClaimController : ControllerBase {
    private readonly IUserService _userService;
    private readonly IClaimService _claimService;
    private readonly IUserClaimService _userClaimService;

    public UserClaimController(IUserService userService, IClaimService claimService, IUserClaimService userClaimService) {
        _userService = userService;
        _claimService = claimService;
        _userClaimService = userClaimService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountClaimDto>>> GetClaims(long userId) {
        var user = await _userService.Find(userId);

        if (user == null) {
            return NotFound();
        }

        return Ok(_userClaimService.GetClaims(user).ToDto());
    }

    [HttpPost("{claimId:long}")]
    public async Task<ActionResult> AddClaim(long userId, long claimId) {
        var user = await _userService.Find(userId);

        if (user == null) {
            return NotFound();
        }

        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        await _userClaimService.AddClaim(claim, user);

        return Ok();
    }

    [HttpDelete("{claimId:long}")]
    public async Task<ActionResult> RemoveClaim(long userId, long claimId) {
        var user = await _userService.Find(userId);

        if (user == null) {
            return NotFound();
        }

        var claim = await _claimService.Find(claimId);

        if (claim == null) {
            return NotFound();
        }

        await _userClaimService.RemoveClaim(claim, user);

        return Ok();
    }
}