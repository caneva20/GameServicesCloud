namespace GameServicesCloud.Accounts;

public interface IClaimService {
    Task<AccountClaim?> Find(long claimId);

    Task<AccountClaim?> Find(string claimName);

    Task<List<AccountClaim>> FindDefaults();

    Task<List<AccountClaim>> FindAll();

    Task<bool> Create(AccountClaim claim);

    Task Remove(AccountClaim claim);
}