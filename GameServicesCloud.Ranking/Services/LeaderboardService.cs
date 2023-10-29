using GameServicesCloud.Data;

namespace GameServicesCloud.Ranking;

public class LeaderboardService : ILeaderboardService {
    private readonly IRepository<Leaderboard> _repository;

    public LeaderboardService(IRepository<Leaderboard> repository) {
        _repository = repository;
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
}