using System.Security.Claims;
using GameServicesCloud.Persistence.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Persistence.Controllers;

[ApiController]
[Route("[controller]")]
public class UserDataController : ControllerBase {
    private readonly IUserDataService _userDataService;

    public UserDataController(IUserDataService userDataService) {
        _userDataService = userDataService;
    }

    [HttpGet]
    public async Task<ActionResult<UserDataDto>> Get() {
        if (!long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)) {
            return Forbid();
        }

        var userData = await _userDataService.Find(userId);

        if (userData == null) {
            return NotFound();
        }

        return userData.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult> Save([FromBody] byte[] data) {
        if (!long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)) {
            return Forbid();
        }

        var userData = await _userDataService.Find(userId) ?? await _userDataService.Create(userId);

        await _userDataService.Save(userData, data);

        return Ok();
    }
}