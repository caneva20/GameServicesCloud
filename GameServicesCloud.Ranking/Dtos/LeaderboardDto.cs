namespace GameServicesCloud.Ranking.Dtos;

public class LeaderboardDto {
    public string Name { get; set; } = null!;
    public int Scores { get; set; }
}

public class CreateLeaderBoardDto {
    public string Name { get; set; } = null!;
}