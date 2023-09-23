using GameServicesCloud.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Accounts.Repositories;

public class UserTokenRepository : Repository<UserToken> {
    private readonly UserTokenOptions _options;

    public UserTokenRepository(DbContext context, IOptions<UserTokenOptions> options) : base(context) {
        _options = options.Value;
    }

    public override Task<UserToken> Save(UserToken entity) {
        entity.ExpirationDate = DateTime.UtcNow.AddSeconds(_options.ExpirationTimeInSeconds);

        return base.Save(entity);
    }

    public override Task SaveAll(IEnumerable<UserToken> entities) {
        var expirationDate = DateTime.UtcNow.AddSeconds(_options.ExpirationTimeInSeconds);
        var userTokens = entities.ToList();

        foreach (var entity in userTokens) {
            entity.ExpirationDate = expirationDate;
        }

        return base.SaveAll(userTokens);
    }

    protected override IQueryable<UserToken> Query(TrackingBehaviour behaviour) {
        return base.Query(behaviour).Where(x => x.ExpirationDate > DateTime.UtcNow);
    }
}