using GameServicesCloud.Data;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Persistence;

public class UserDataService : IUserDataService {
    private readonly ILogger<UserDataService> _logger;

    private readonly IRepository<UserData> _userDataRepository;
    private readonly UserDataOptions _options;

    public UserDataService(ILogger<UserDataService> logger, IRepository<UserData> userDataRepository, IOptions<UserDataOptions> options) {
        _userDataRepository = userDataRepository;
        _logger = logger;
        _options = options.Value;
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

    public async Task<bool> Save(UserData userData, byte[] data) {
        if (data.Length > _options.MaxByteSize) {
            _logger.LogInformation("Failed to save user data of {Size} as it exceeds the limit of {CurrentLimit}",
                DataUnitConverter.Convert(data.Length),
                DataUnitConverter.Convert(_options.MaxByteSize));

            return false;
        }

        userData.Data = data;

        await _userDataRepository.Update(userData);

        return true;
    }
}