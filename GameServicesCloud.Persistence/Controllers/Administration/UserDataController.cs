using System.Linq.Expressions;
using GameServicesCloud.Controllers;
using GameServicesCloud.Data;
using GameServicesCloud.Persistence.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Persistence.Controllers.Administration;

[Authorize]
[ApiController]
[Route("admin/[controller]")]
public class UserDataController : PaginationController<UserData, UserDataDto> {
    private readonly IUserDataService _userDataService;

    public UserDataController(IPaginator<UserData> paginator, IUserDataService userDataService) : base(paginator) {
        _userDataService = userDataService;
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserData(long userId) {
        var userData = await _userDataService.Find(userId);

        if (userData == null) {
            return NotFound();
        }

        await _userDataService.Save(userData, Array.Empty<byte>());

        return Ok();
    }

    protected override Expression<Func<UserData, bool>> Filter(string filter) {
        return x => x.UserId.ToString().Contains(filter);
    }

    protected override UserDataDto ConvertToDto(UserData entity) {
        return entity.ToDto();
    }
}