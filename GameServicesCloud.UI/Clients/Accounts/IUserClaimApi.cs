using Refit;

namespace GameServicesCloud.UI.Clients.Accounts;

[Headers("Authorization: Bearer")]
public interface IUserClaimApi {
    [Post("/admin/userclaim/{userId}/{claimId}")]
    Task AddClaim(long userId, long claimId);

    [Delete("/admin/userclaim/{userId}/{claimId}")]
    Task RemoveClaim(long userId, long claimId);
}