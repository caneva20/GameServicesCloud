namespace GameServicesCloud.Accounts;

public interface IClaimService {
    Task<AccountClaim?> Find(long claimId);
    Task<List<AccountClaim>> FindAll();
    Task<bool> Create(AccountClaim claim);
    Task Remove(AccountClaim claim);
}