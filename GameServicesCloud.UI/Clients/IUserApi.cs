using Refit;

namespace GameServicesCloud.UI.Clients;

[Headers("Authorization: Bearer")]
public interface IUserApi {
    [Get("/admin/user/{id}")]
    Task<User> Find(long id);

    [Get("/admin/user")]
    Task<IEnumerable<User>> FindAll([Query] int page, [Query] int pageSize, [Query] string filter);

    [Get("/admin/user/count")]
    Task<int> Count([Query] string filter);
}