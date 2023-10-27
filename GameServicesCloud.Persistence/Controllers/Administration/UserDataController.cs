using GameServicesCloud.Data;
using GameServicesCloud.Persistence.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Persistence.Controllers.Administration;

[Authorize]
[ApiController]
[Route("admin/[controller]")]
public class UserDataController : ControllerBase {
    private readonly IPaginator<UserData> _paginator;

    public UserDataController(IPaginator<UserData> paginator) {
        _paginator = paginator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDataAdminDto>> FindAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? filter = null) {
        return (await _paginator.List(page, pageSize, x => filter == null || x.UserId.ToString().Contains(filter))).ToAdminDto();
    }

    [HttpGet("count")]
    public async Task<int> Count([FromQuery] string? filter = null) {
        return await _paginator.Count(x => filter == null || x.UserId.ToString().Contains(filter));
    }
}