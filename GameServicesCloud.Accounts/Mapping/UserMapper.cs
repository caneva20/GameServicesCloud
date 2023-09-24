using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Accounts.Mapping;

[Mapper]
public static partial class UserMapper {
    public static partial User ToEntity(this UserDto dto);
}