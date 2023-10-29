using GameServicesCloud.Ranking.Dtos;
using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Ranking.Mapping;

[Mapper]
public static partial class LeaderboardMapper {
    public static partial LeaderboardDto ToDto(this Leaderboard entity);
}