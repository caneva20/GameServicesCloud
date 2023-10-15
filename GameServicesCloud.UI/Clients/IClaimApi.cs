using Refit;

namespace GameServicesCloud.UI.Clients;

[Headers("Authorization: Bearer")]
public interface IClaimApi {
    [Get("/claim")]
    Task<IEnumerable<AccountClaim>> FindAll([Query] int page, [Query] int pageSize, [Query] string filter);

    [Get("/claim/count")]
    Task<int> Count([Query] string filter);

    [Put("/claim/{claimId}")]
    Task SetDefault(long claimId, [Body] bool isDefault);
}