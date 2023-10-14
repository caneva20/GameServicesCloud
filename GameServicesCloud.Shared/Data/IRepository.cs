using System.Linq.Expressions;

namespace GameServicesCloud.Data;

public interface IRepository<T> where T : IEntity {
    Task<List<T>> FindAll(TrackingBehaviour behaviour = TrackingBehaviour.NoTracking);

    Task<List<T>> FindAll(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.NoTracking);

    Task<T?> Find(Expression<Func<T, bool>> predicate, TrackingBehaviour behaviour = TrackingBehaviour.Tracking);

    Task<T?> Find(long id, TrackingBehaviour behaviour = TrackingBehaviour.Tracking);

    Task<bool> Exists(Expression<Func<T, bool>> predicate);

    Task<bool> Exists(long id);

    Task<T> Save(T entity);

    Task SaveAll(IEnumerable<T> entities);

    Task<T> Update(T entity);

    Task UpdateAll(IEnumerable<T> entities);

    Task Remove(T entity);

    Task RemoveAll(IEnumerable<T> entities);

    IQueryable<T> Query(TrackingBehaviour behaviour);
}