using GameServicesCloud.Data;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Accounts.Repositories;

public class UserRepository : Repository<User> {
    public UserRepository(DbContext context) : base(context) {
    }

    public override IQueryable<User> Query(TrackingBehaviour behaviour) {
        return base.Query(behaviour).Include(x => x.Claims);
    }
}