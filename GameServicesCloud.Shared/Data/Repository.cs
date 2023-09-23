using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Data;

public class Repository<T> : IRepository<T> where T : class, IEntity {
    private readonly DbContext _context;

    public Repository(DbContext context) {
        _context = context;
    }

    public Task<List<T>> FindAll(TrackingBehaviour behaviour = TrackingBehaviour.NoTracking) {
        return Query(behaviour).ToListAsync();
    }

    public Task<List<T>> FindAll(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.NoTracking) {
        return Query(behaviour).Where(predicate).ToListAsync();
    }

    public Task<T?> Find(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.Tracking) {
        return Query(behaviour).Where(predicate).SingleOrDefaultAsync();
    }

    public Task<T?> Find(long id, TrackingBehaviour behaviour = TrackingBehaviour.Tracking) {
        return Query(behaviour).Where(x => x.Id == id).SingleOrDefaultAsync();
    }

    public Task<bool> Exists(Expression<Func<T, bool>> predicate) {
        return Query(TrackingBehaviour.NoTracking).AnyAsync(predicate);
    }

    public Task<bool> Exists(long id) {
        return Query(TrackingBehaviour.NoTracking).AnyAsync(x => x.Id == id);
    }

    public async Task<T> Save(T entity) {
        var entry = _context.Add(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task SaveAll(IEnumerable<T> entity) {
        _context.AddRange(entity);

        await _context.SaveChangesAsync();
    }

    protected IQueryable<T> Query(TrackingBehaviour behaviour) {
        return behaviour switch {
            TrackingBehaviour.Tracking => _context.Set<T>().AsTracking(),
            TrackingBehaviour.NoTracking => _context.Set<T>().AsNoTracking(),
            TrackingBehaviour.NoTrackingWithIdentityResolution => _context.Set<T>().AsNoTrackingWithIdentityResolution(),
            _ => throw new ArgumentOutOfRangeException(nameof(behaviour), behaviour, null)
        };
    }
}