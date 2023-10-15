using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace GameServicesCloud;

public partial class ControllerClaimProviderService : IControllerClaimProviderService {
    private readonly ILogger<ControllerClaimProviderService> _logger;
    private readonly IActionDescriptorCollectionProvider _actionDescriptorCollection;
    private string _prefix = null!;

    private readonly Dictionary<string, IReadOnlyList<string>> _controllerClaims = new();

    private static readonly Dictionary<string, string> SuffixMap = new() {
        { "GET", "read" },
        { "POST", "create" },
        { "PUT", "update" },
        { "PATCH", "update" },
        { "DELETE", "delete" }
    };

    public ControllerClaimProviderService(ILogger<ControllerClaimProviderService> logger, IActionDescriptorCollectionProvider actionDescriptorCollection) {
        _logger = logger;
        _actionDescriptorCollection = actionDescriptorCollection;
    }

    public void Initialize(string prefix) {
        _controllerClaims.Clear();
        _prefix = prefix.ToLower();

        foreach (var descriptor in _actionDescriptorCollection.ActionDescriptors.Items) {
            if (descriptor is not ControllerActionDescriptor controllerDescriptor) {
                return;
            }

            _controllerClaims.Add(descriptor.Id, BuildClaims(controllerDescriptor).ToArray());
        }
    }

    public IReadOnlyCollection<string> GetRequiredClaims(ActionDescriptor descriptor) {
        return _controllerClaims[descriptor.Id];
    }

    private IEnumerable<string> BuildClaims(ControllerActionDescriptor descriptor) {
        var controller = descriptor.ControllerName;
        var action = descriptor.AttributeRouteInfo?.Template?.Replace(controller, "") ?? "";

        action = RouteParameterRegex().Replace(action, "");
        action = SlashRegex().Replace(action, ".");

        if (action.StartsWith('.')) {
            action = action[1..];
        }

        var methods = descriptor.ActionConstraints?.OfType<HttpMethodActionConstraint>().SelectMany(x => x.HttpMethods).Distinct();

        if (methods == null) {
            yield break;
        }

        foreach (var method in methods) {
            yield return BuildClaimName(_prefix, controller, action, method);
        }
    }

    private string BuildClaimName(string prefix, string controller, string action, string method) {
        var sb = new StringBuilder();
        sb.Append(prefix.ToLower());
        sb.Append('.');
        sb.Append(controller.ToLower());
        sb.Append('.');

        if (!string.IsNullOrEmpty(action)) {
            sb.Append(action.ToLower());
            sb.Append('.');
        }

        if (SuffixMap.TryGetValue(method, out var suffix)) {
            sb.Append(suffix);
        } else {
            _logger.LogWarning("Method {Method} could not be mapped to a claim suffix", method);

            sb.Append(method.ToLower());
        }

        return sb.ToString();
    }

    [GeneratedRegex(@"\{.*?\}")]
    private static partial Regex RouteParameterRegex();

    [GeneratedRegex(@"\/+")]
    private static partial Regex SlashRegex();
}