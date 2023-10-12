using GameServicesCloud.Data;

namespace GameServicesCloud.Accounts;

public class UserClaimService : IUserClaimService {
    private readonly IRepository<User> _userRepository;

    public UserClaimService(IRepository<User> userRepository) {
        _userRepository = userRepository;
    }

    public IEnumerable<AccountClaim> GetClaims(User user) {
        return user.Claims;
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