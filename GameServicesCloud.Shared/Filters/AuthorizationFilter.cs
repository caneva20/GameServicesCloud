using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GameServicesCloud.Filters;

public class AuthorizationFilter : IAuthorizationFilter {
    private readonly ILogger<AuthorizationFilter> _logger;
    private readonly IControllerClaimProviderService _controllerClaimProviderService;

    public AuthorizationFilter(IControllerClaimProviderService controllerClaimProviderService, ILogger<AuthorizationFilter> logger) {
        _controllerClaimProviderService = controllerClaimProviderService;
        _logger = logger;
    }

    public void OnAuthorization(AuthorizationFilterContext context) {
        if (context.ActionDescriptor is not ControllerActionDescriptor actionDescriptor) {
            return;
        }

        if (actionDescriptor.EndpointMetadata.Any(x => x is AllowAnonymousAttribute)) {
            return;
        }

        var claims = _controllerClaimProviderService.GetRequiredClaims(actionDescriptor);

        foreach (var claim in claims) {
            if (context.HttpContext.User.Claims.Any(x => x.Type == claim)) {
                continue;
            }

            context.Result = new ForbidResult();

            _logger.LogInformation("Required claim {Claim} not found for {User}", claim, context.HttpContext.User.Identity?.Name);

            break;
        }
    }
}