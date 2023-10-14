namespace GameServicesCloud.UI;

public record User(long Id, string Email, List<AccountClaim> Claims);