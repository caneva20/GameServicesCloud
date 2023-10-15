using Microsoft.AspNetCore.Mvc.Abstractions;

namespace GameServicesCloud;

public interface IControllerClaimProviderService {
    void Initialize(string prefix);

    IEnumerable<string> GetRequiredClaims(ActionDescriptor descriptor);

    IEnumerable<string> Claims { get; }
}