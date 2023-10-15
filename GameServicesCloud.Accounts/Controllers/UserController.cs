using GameServicesCloud.Accounts.Mapping;
using GameServicesCloud.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    private readonly IPaginator<User> _paginator;
    private readonly IUserService _userService;

    public UserController(IPaginator<User> paginator, IUserService userService) {
        _paginator = paginator;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> FindAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string filter = "") {
        return (await _paginator.List(page, pageSize, x => x.Email.Contains(filter))).ToDto();
    }

    [HttpGet("count")]
    public Task<int> FindTotal([FromQuery] string filter = "") {
        return _paginator.Count(x => x.Email.Contains(filter));
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserDto>> Find(long id) {
        var user = await _userService.Find(id);

        if (user == null) {
            return NotFound();
        }

        return Ok(user.ToDto());
    }
}