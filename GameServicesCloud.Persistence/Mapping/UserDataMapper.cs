using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Persistence.Mapping;

[Mapper]
public static partial class UserDataMapper {
    public static partial UserDataDto ToDto(this UserData userData);
}