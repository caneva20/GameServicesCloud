using GameServicesCloud.Data;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Ranking;

public class LeaderboardService : ILeaderboardService {
    private readonly IRepository<Leaderboard> _repository;
    private readonly IRepository<UserScore> _userScoreRepository;

    public LeaderboardService(IRepository<Leaderboard> repository, IRepository<UserScore> userScoreRepository) {
        _repository = repository;
        _userScoreRepository = userScoreRepository;
    }

    public Task<Leaderboard?> Find(string name) {
        return _repository.Find(x => x.Name == name);
    }

    public async Task<bool> Create(string name) {
        if (await _repository.Exists(x => x.Name == name)) {
            return false;
        }

        var leaderboard = new Leaderboard {
            Name = name
        };

        await _repository.Save(leaderboard);

        return true;
    }

    public Task Delete(Leaderboard leaderboard) {
        return _repository.Remove(leaderboard);
    }

    public async Task<IEnumerable<LeaderboardPosition>> GetLeaderboard(Leaderboard leaderboard, int page = 0, int pageSize = 100) {
        var userScores = await _userScoreRepository.Query(TrackingBehaviour.NoTracking)
            .Where(x => x.Leaderboard == leaderboard)
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.UserId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return userScores.Select((x, i) => new LeaderboardPosition(x.UserId, x.Score, i + 1 + page * pageSize));
    }

    public async Task<int> GetLeaderboardCount(Leaderboard leaderboard) {
        return await _userScoreRepository.Count(x => x.Leaderboard == leaderboard);
    }

    public async Task SaveScore(Leaderboard leaderboard, long userId, double score) {
        var userScore = await _userScoreRepository.Find(x => x.Leaderboard == leaderboard && x.UserId == userId);

        if (userScore != null) {
            userScore.Score = score;

            await _userScoreRepository.Update(userScore);
        } else {
            await _userScoreRepository.Save(new UserScore {
                UserId = userId,
                Leaderboard = leaderboard,
                Score = score
            });
        }
    }
}