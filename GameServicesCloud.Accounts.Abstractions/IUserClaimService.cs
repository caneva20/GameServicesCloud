namespace GameServicesCloud.Accounts;

public interface IUserClaimService {
    IEnumerable<AccountClaim> GetClaims(User user);
    Task AddClaim(AccountClaim claim, User user);
    Task RemoveClaim(AccountClaim claim, User user);
}