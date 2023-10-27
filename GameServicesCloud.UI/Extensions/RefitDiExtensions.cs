using GameServicesCloud.UI.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace GameServicesCloud.UI.Extensions;

public static class RefitDiExtensions {
    public static IHttpClientBuilder AddRefitBackendClient<T>(this WebAssemblyHostBuilder builder, string backendName, bool addAuth = true) where T : class {
        var clientBuilder = builder.Services.AddRefitClient<T>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.Configuration[$"Backend:{backendName}BaseUrl"]!));

        if (addAuth) {
            clientBuilder = clientBuilder.AddHttpMessageHandler<AuthHeaderHandler>();
        }

        return clientBuilder;
    }
}