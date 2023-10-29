using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GameServicesCloud.Data;

public class Paginator<T> : IPaginator<T> where T : IEntity {
    private readonly IRepository<T> _repository;

    public Paginator(IRepository<T> repository) {
        _repository = repository;
    }

    public virtual Task<List<T>> List(int page, int pageSize, Expression<Func<T, bool>> predicate) {
        return _repository.Query(TrackingBehaviour.NoTracking).Where(predicate).OrderBy(x => x.Id).Skip(page * pageSize).Take(pageSize).ToListAsync();
    }

    public virtual Task<List<T>> List(int page, int pageSize) {
        return _repository.Query(TrackingBehaviour.NoTracking).OrderBy(x => x.Id).Skip(page * pageSize).Take(pageSize).ToListAsync();
    }

    public virtual Task<int> Count(Expression<Func<T, bool>> predicate) {
        return _repository.Query(TrackingBehaviour.NoTracking).CountAsync(predicate);
    }

    public virtual Task<int> Count() {
        return _repository.Query(TrackingBehaviour.NoTracking).CountAsync();
    }
}