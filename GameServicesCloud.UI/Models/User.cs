namespace GameServicesCloud.UI;

public record User(long Id, string Email, IReadOnlyCollection<AccountClaim> Claims);