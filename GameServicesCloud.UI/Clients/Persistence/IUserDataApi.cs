using Refit;

namespace GameServicesCloud.UI.Clients.Persistence;

public interface IUserDataApi {
    [Get("/admin/userdata")]
    Task<IEnumerable<UserData>> FindAll([Query] int page, [Query] int pageSize, [Query] string filter);

    [Get("/admin/userdata/count")]
    Task<int> Count([Query] string filter);
}