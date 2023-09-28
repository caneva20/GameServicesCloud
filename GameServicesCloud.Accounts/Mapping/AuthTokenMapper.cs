using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Accounts.Mapping;

[Mapper]
public static partial class AuthTokenMapper {
    public static partial AuthTokenDto ToDto(this AuthToken token);
}