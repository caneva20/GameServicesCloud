using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Data;

public class Paginator<T> : IPaginator<T> where T : IEntity {
    private readonly IRepository<T> _repository;

    public Paginator(IRepository<T> repository) {
        _repository = repository;
    }

    public virtual Task<List<T>> List(int page, int pageSize, Expression<Func<T, bool>> predicate) {
        return _repository.Query(TrackingBehaviour.NoTracking).Where(predicate).Skip(page * pageSize).Take(pageSize).ToListAsync();
    }

    public virtual Task<int> Count(Expression<Func<T, bool>> predicate) {
        return _repository.Query(TrackingBehaviour.NoTracking).CountAsync(predicate);
    }
}