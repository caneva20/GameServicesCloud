using Refit;

namespace GameServicesCloud.UI.Clients;

[Headers("Authorization: Bearer")]
public interface IClaimApi {
    [Get("/admin/claim")]
    Task<IEnumerable<AccountClaim>> FindAll([Query] int page, [Query] int pageSize, [Query] string filter);

    [Get("/admin/claim/count")]
    Task<int> Count([Query] string filter);

    [Put("/admin/claim/{claimId}")]
    Task SetDefault(long claimId, [Body] bool isDefault);
}