using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Ranking.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/{name}")]
public class LeaderboardController : ControllerBase {
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILeaderboardService leaderboardService) {
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public async Task<ActionResult> GetLeaderboard(string name, int page = 0, int pageSize = 100) {
        var leaderboard = await _leaderboardService.Find(name);

        if (leaderboard == null) {
            return NotFound();
        }

        var positions = await _leaderboardService.GetLeaderboard(leaderboard, page, pageSize);

        return Ok(positions);
    }

    [HttpPost]
    public async Task<ActionResult> SaveScore(string name, [FromBody] int score) {
        if (!long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)) {
            return Forbid();
        }

        var leaderboard = await _leaderboardService.Find(name);

        if (leaderboard == null) {
            return NotFound();
        }

        await _leaderboardService.SaveScore(leaderboard, userId, score);

        return Ok();
    }
}