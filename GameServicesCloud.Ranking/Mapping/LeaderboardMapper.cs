using GameServicesCloud.Ranking.Dtos;
using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Ranking.Mapping;

[Mapper]
public static partial class LeaderboardMapper {
    public static LeaderboardDto ToDto(this Leaderboard entity) {
        var dto = MapToDto(entity);

        dto.Scores = entity.Scores.Count;

        return dto;
    }

    private static partial LeaderboardDto MapToDto(this Leaderboard entity);
}