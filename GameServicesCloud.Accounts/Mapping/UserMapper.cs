using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Accounts.Mapping;

[Mapper]
public static partial class UserMapper {
    public static partial User ToEntity(this UserDto dto);

    public static partial UserDto ToDto(this User entity);

    public static partial IEnumerable<UserDto> ToDto(this IEnumerable<User> entity);

    public static partial User ToEntity(this CreateUserDto dto);
}