using System.Linq.Expressions;

namespace GameServicesCloud.Data;

public interface IPaginator<T> where T : IEntity {
    Task<List<T>> List(int page, int pageSize, Expression<Func<T, bool>> predicate);

    Task<int> Count(Expression<Func<T, bool>> predicate);
}