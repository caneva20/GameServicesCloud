using Refit;

namespace GameServicesCloud.UI.Clients;

[Headers("Authorization: Bearer")]
public interface IUserClaimApi {
    [Post("/userclaim/{userId}/{claimId}")]
    Task AddClaim(long userId, long claimId);

    [Delete("/userclaim/{userId}/{claimId}")]
    Task RemoveClaim(long userId, long claimId);
}