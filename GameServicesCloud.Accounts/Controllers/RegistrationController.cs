using GameServicesCloud.Accounts.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase {
    private readonly ILogger<RegistrationController> _logger;
    private readonly IUserService _userService;

    public RegistrationController(ILogger<RegistrationController> logger, IUserService userService) {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("Registered")]
    public async Task<bool> IsRegistered([FromQuery] string email) {
        _logger.LogDebug("Checking registration status of {Email}", email);

        return await _userService.IsRegistered(email);
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] UserDto userDto) {
        var user = userDto.ToEntity();

        var registeredUser = await _userService.RegisterUser(user);

        if (registeredUser == null) {
            return BadRequest("Failed to register user");
        }

        return Created("TBD", registeredUser);
    }
}