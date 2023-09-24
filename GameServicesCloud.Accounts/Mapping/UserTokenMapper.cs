using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Accounts.Mapping;

[Mapper]
public static partial class UserTokenMapper {
    public static partial UserTokenDto ToDto(this UserToken userToken);

    public static partial IEnumerable<UserTokenDto> ToDto(this IEnumerable<UserToken> userTokens);
}