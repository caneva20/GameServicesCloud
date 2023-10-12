using Riok.Mapperly.Abstractions;

namespace GameServicesCloud.Accounts.Mapping;

[Mapper]
public static partial class AccountClaimMapper {
    public static partial AccountClaimDto ToDto(this AccountClaim claim);

    public static partial IEnumerable<AccountClaimDto> ToDto(this IEnumerable<AccountClaim> claim);

    public static partial AccountClaim ToEntity(this CreateAccountClaimDto dto);
}