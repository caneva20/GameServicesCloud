namespace GameServicesCloud.Accounts;

public interface IClaimService {
    Task<AccountClaim?> Find(long claimId);
    Task<List<AccountClaim>> FindAll();
    Task<bool> Create(AccountClaim claim);
    Task Remove(AccountClaim claim);
    Task<AccountClaim?> Find(string claimName);
    Task<List<AccountClaim>> FindAll(IEnumerable<string> claimNames);
}