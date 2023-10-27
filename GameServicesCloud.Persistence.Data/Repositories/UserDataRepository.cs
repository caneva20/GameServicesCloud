using GameServicesCloud.Data;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Persistence;

public class UserDataRepository : Repository<UserData> {
    public UserDataRepository(DbContext context) : base(context) {
    }

    public override Task<UserData> Save(UserData entity) {
        entity.UpdatedAt = DateTime.UtcNow;

        return base.Save(entity);
    }

    public override Task SaveAll(IEnumerable<UserData> entities) {
        var userDatas = entities.ToList();
        foreach (var entity in userDatas) {
            entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.SaveAll(userDatas);
    }

    public override Task<UserData> Update(UserData entity) {
        entity.UpdatedAt = DateTime.UtcNow;

        return base.Update(entity);
    }

    public override Task UpdateAll(IEnumerable<UserData> entities) {
        var userDatas = entities.ToList();
        foreach (var entity in userDatas) {
            entity.UpdatedAt = DateTime.UtcNow;
        }

        return base.UpdateAll(userDatas);
    }
}