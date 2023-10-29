using System.Linq.Expressions;
using GameServicesCloud.Controllers;
using GameServicesCloud.Data;
using GameServicesCloud.Ranking.Dtos;
using GameServicesCloud.Ranking.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Ranking.Controllers.Administration;

[Authorize]
[ApiController]
[Route("admin/[controller]")]
public class LeaderboardController : PaginationController<Leaderboard, LeaderboardDto> {
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(IPaginator<Leaderboard> paginator, ILeaderboardService leaderboardService) : base(paginator) {
        _leaderboardService = leaderboardService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateLeaderboard([FromBody] CreateLeaderBoardDto dto) {
        var created = await _leaderboardService.Create(dto.Name);

        if (!created) {
            return BadRequest();
        }

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteLeaderboard([FromQuery] string name) {
        var leaderboard = await _leaderboardService.Find(name);

        if (leaderboard == null) {
            return NotFound();
        }

        await _leaderboardService.Delete(leaderboard);

        return Ok();
    }

    protected override Expression<Func<Leaderboard, bool>> Filter(string filter) {
        return x => x.Name.Contains(filter);
    }

    protected override LeaderboardDto ConvertToDto(Leaderboard entity) {
        return entity.ToDto();
    }
}