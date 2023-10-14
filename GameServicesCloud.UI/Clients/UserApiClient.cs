using Refit;

namespace GameServicesCloud.UI.Clients;

[Headers("Authorization: Bearer")]
public interface IUserApi {
    [Get("/user/{id}")]
    Task<User> Find(long id);

    [Get("/user")]
    Task<IEnumerable<User>> FindAll([Query] int page, [Query] int pageSize, [Query] string filter);

    [Get("/user/count")]
    Task<int> Count([Query] string filter);
}