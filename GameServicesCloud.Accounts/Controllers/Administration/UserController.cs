using System.Linq.Expressions;
using GameServicesCloud.Accounts.Mapping;
using GameServicesCloud.Controllers;
using GameServicesCloud.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Accounts.Controllers.Administration;

[ApiController]
[Route("admin/[controller]")]
public class UserController : PaginationController<User, UserDto> {
    private readonly IUserService _userService;

    public UserController(IPaginator<User> paginator, IUserService userService) : base(paginator) {
        _userService = userService;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserDto>> Find(long id) {
        var user = await _userService.Find(id);

        if (user == null) {
            return NotFound();
        }

        return Ok(user.ToDto());
    }

    protected override Expression<Func<User, bool>> Filter(string filter) {
        return x => x.Email.Contains(filter);
    }

    protected override UserDto ConvertToDto(User entity) {
        return entity.ToDto();
    }
}