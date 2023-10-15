using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class ClaimService : IClaimService {
    private readonly IRepository<AccountClaim> _repository;

    public ClaimService(IRepository<AccountClaim> repository) {
        _repository = repository;
    }

    public Task<AccountClaim?> Find(long claimId) {
        return _repository.Find(claimId);
    }

    public Task<AccountClaim?> Find(string claimName) {
        return _repository.Find(x => x.Name == claimName);
    }

    public Task<List<AccountClaim>> FindDefaults() {
        return _repository.FindAll(x => x.IsDefault);
    }

    public Task<List<AccountClaim>> FindAll() {
        return _repository.FindAll();
    }

    public async Task<bool> Create(AccountClaim claim) {
        if (await _repository.Exists(x => x.Name == claim.Name)) {
            return false;
        }

        await _repository.Save(claim);

        return true;
    }

    public Task Remove(AccountClaim claim) {
        return _repository.Remove(claim);
    }

    public Task SetDefault(AccountClaim claim, bool isDefault) {
        claim.IsDefault = isDefault;

        return _repository.Update(claim);
    }
}