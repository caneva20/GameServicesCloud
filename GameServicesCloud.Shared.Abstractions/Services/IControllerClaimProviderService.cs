using Microsoft.AspNetCore.Mvc.Abstractions;

namespace GameServicesCloud;

public interface IControllerClaimProviderService {
    void Initialize(string prefix);

    IReadOnlyCollection<string> GetRequiredClaims(ActionDescriptor descriptor);
}