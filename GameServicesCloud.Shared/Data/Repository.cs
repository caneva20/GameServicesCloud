using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Data;

public class Repository<T> : IRepository<T> where T : class, IEntity {
    private readonly DbContext _context;

    public Repository(DbContext context) {
        _context = context;
    }

    public virtual Task<List<T>> FindAll(TrackingBehaviour behaviour = TrackingBehaviour.NoTracking) {
        return Query(behaviour).ToListAsync();
    }

    public virtual Task<List<T>> FindAll(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.NoTracking) {
        return Query(behaviour).Where(predicate).ToListAsync();
    }

    public virtual Task<T?> Find(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.Tracking) {
        return Query(behaviour).Where(predicate).SingleOrDefaultAsync();
    }

    public virtual Task<T?> Find(long id, TrackingBehaviour behaviour = TrackingBehaviour.Tracking) {
        return Query(behaviour).Where(x => x.Id == id).SingleOrDefaultAsync();
    }

    public virtual Task<bool> Exists(Expression<Func<T, bool>> predicate) {
        return Query(TrackingBehaviour.NoTracking).AnyAsync(predicate);
    }

    public virtual Task<bool> Exists(long id) {
        return Query(TrackingBehaviour.NoTracking).AnyAsync(x => x.Id == id);
    }

    public virtual async Task<T> Save(T entity) {
        var entry = _context.Add(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public virtual async Task SaveAll(IEnumerable<T> entities) {
        _context.AddRange(entities);

        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> Update(T entity) {
        var entry = _context.Update(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public virtual async Task UpdateAll(IEnumerable<T> entities) {
        _context.UpdateRange(entities);

        await _context.SaveChangesAsync();
    }

    public async Task Remove(T entity) {
        _context.Remove(entity);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveAll(IEnumerable<T> entities) {
        _context.RemoveRange(entities);

        await _context.SaveChangesAsync();
    }

    public virtual IQueryable<T> Query(TrackingBehaviour behaviour) {
        return behaviour switch {
            TrackingBehaviour.Tracking => _context.Set<T>().AsTracking(),
            TrackingBehaviour.NoTracking => _context.Set<T>().AsNoTracking(),
            TrackingBehaviour.NoTrackingWithIdentityResolution => _context.Set<T>().AsNoTrackingWithIdentityResolution(),
            _ => throw new ArgumentOutOfRangeException(nameof(behaviour), behaviour, null)
        };
    }
}