﻿using GameServicesCloud.Data;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Accounts;

public class UserTokenService : IUserTokenService {
    private readonly ILogger<UserTokenService> _logger;
    private readonly UserTokenOptions _options;
    private readonly ITokenService _tokenService;
    private readonly IRepository<UserToken> _userTokenRepository;

    public UserTokenService(ILogger<UserTokenService> logger,
        ITokenService tokenService,
        IRepository<UserToken> userTokenRepository,
        IOptions<UserTokenOptions> options) {
        _logger = logger;
        _tokenService = tokenService;
        _userTokenRepository = userTokenRepository;
        _options = options.Value;
    }

    public Task<UserToken?> Find(User user, string token) {
        return _userTokenRepository.Find(x => x.User == user && x.Token == token);
    }

    public Task Delete(UserToken token) {
        return _userTokenRepository.Remove(token);
    }

    public async Task<UserToken?> GenerateToken(User user) {
        var tries = _options.MaxRetries;

        while (tries-- > 0) {
            var token = _tokenService.GenerateToken(_options.TokenLength, _options.TokenCharacters);

            if (await _userTokenRepository.Exists(x => x.User == user && x.Token == token)) {
                continue;
            }

            return await SaveToken(user, token);
        }

        _logger.LogWarning("Failed to generate token for user {UserId} after {Retries} tries", user.Id, _options.MaxRetries);

        return null;
    }

    private async Task<UserToken> SaveToken(User user, string token) {
        var userToken = await _userTokenRepository.Save(new UserToken {
            User = user,
            Token = token
        });

        _logger.LogDebug("New token generated for user {UserId}", user.Id);

        return userToken;
    }
}