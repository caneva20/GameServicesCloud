using GameServicesCloud.Accounts.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController : ControllerBase {
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, IUserService userService) {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> StartLogin([FromQuery] string email) {
        var user = await _userService.Find(email);

        if (user == null) {
            return NotFound();
        }

        var generated = await _authService.GenerateLoginToken(user);

        if (!generated) {
            return Conflict();
        }

        return Ok();
    }

    [HttpPost("Token")]
    public async Task<ActionResult<AuthTokenDto>> FinishLogin([FromBody] AuthLoginRequestDto request) {
        var user = await _userService.Find(request.Email);

        if (user == null) {
            return NotFound();
        }

        var authToken = await _authService.GenerateAuthToken(user, request.LoginToken);

        if (authToken == null) {
            return BadRequest();
        }

        return authToken.ToDto();
    }
}