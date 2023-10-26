using GameServicesCloud.Data;

namespace GameServicesCloud.Persistence;

public class UserDataService : IUserDataService {
    private readonly IRepository<UserData> _userDataRepository;

    public UserDataService(IRepository<UserData> userDataRepository) {
        _userDataRepository = userDataRepository;
    }

    public Task<UserData?> Find(long userId) {
        return _userDataRepository.Find(x => x.UserId == userId);
    }

    public async Task<UserData> Create(long userId) {
        var userData = await Find(userId);

        if (userData != null) {
            return userData;
        }

        return await _userDataRepository.Save(new UserData { UserId = userId, Data = Array.Empty<byte>() });
    }

    public async Task Save(UserData userData, byte[] data) {
        userData.Data = data;

        await _userDataRepository.Update(userData);
    }
}