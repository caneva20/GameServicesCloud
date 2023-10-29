using System.Linq.Expressions;
using GameServicesCloud.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameServicesCloud.Controllers;

public abstract class PaginationController<T, TDto> : ControllerBase where T : IEntity {
    private readonly IPaginator<T> _paginator;

    protected PaginationController(IPaginator<T> paginator) {
        _paginator = paginator;
    }

    [HttpGet]
    public async Task<IEnumerable<TDto>> FindAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? filter = null) {
        var entities = filter == null ? await _paginator.List(page, pageSize) : await _paginator.List(page, pageSize, Filter(filter));

        return entities.Select(ConvertToDto);
    }

    [HttpGet("count")]
    public async Task<int> Count([FromQuery] string? filter = null) {
        return filter == null ? await _paginator.Count() : await _paginator.Count(Filter(filter));
    }

    protected abstract Expression<Func<T, bool>> Filter(string filter);

    protected abstract TDto ConvertToDto(T entity);
}