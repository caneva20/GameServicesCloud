using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class UserClaimService : IUserClaimService {
    private readonly IRepository<User> _userRepository;
    private readonly IClaimService _claimService;

    public UserClaimService(IRepository<User> userRepository, IClaimService claimService) {
        _userRepository = userRepository;
        _claimService = claimService;
    }

    public IEnumerable<AccountClaim> GetClaims(User user) {
        return user.Claims;
    }

    public async Task<IEnumerable<AccountClaim>> GetDefaultClaims() {
        return await _claimService.FindAll(Claims.DefaultClaims);
    }

    public async Task<IEnumerable<AccountClaim>> GetAllClaims() {
        return await _claimService.FindAll(Claims.AllClaims);
    }

    public async Task AddClaim(AccountClaim claim, User user) {
        if (user.Claims.Contains(claim)) {
            return;
        }

        user.Claims.Add(claim);

        await _userRepository.Update(user);
    }

    public async Task RemoveClaim(AccountClaim claim, User user) {
        if (!user.Claims.Contains(claim)) {
            return;
        }

        user.Claims.Remove(claim);

        await _userRepository.Update(user);
    }
}