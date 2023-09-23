using System.Linq.Expressions;

namespace GameServicesCloud.Data;

public interface IRepository<T> where T : IEntity {
    public Task<List<T>> FindAll(TrackingBehaviour behaviour = TrackingBehaviour.NoTracking);

    public Task<List<T>> FindAll(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.NoTracking);

    public Task<T?> Find(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.Tracking);

    public Task<T?> Find(long id, TrackingBehaviour behaviour = TrackingBehaviour.Tracking);

    public Task<bool> Exists(Expression<Func<T, bool>> predicate);

    public Task<bool> Exists(long id);

    public Task<T> Save(T entity);

    public Task SaveAll(IEnumerable<T> entity);
}