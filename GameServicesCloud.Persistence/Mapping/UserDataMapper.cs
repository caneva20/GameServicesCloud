using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Persistence.Mapping;

[Mapper]
public static partial class UserDataMapper {
    public static partial UserDataDto ToDto(this UserData userData);

    public static UserDataAdminDto ToAdminDto(this UserData userData) {
        var dto = MapToAdminDto(userData);

        dto.DataSize = userData.Data.Length;

        return dto;
    }

    public static partial IEnumerable<UserDataAdminDto> ToAdminDto(this IEnumerable<UserData> userData);

    [MapperIgnoreSource(nameof(UserData.Data))]
    private static partial UserDataAdminDto MapToAdminDto(UserData userData);
}