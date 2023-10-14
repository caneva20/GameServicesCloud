using GameServicesCloud.UI.Clients;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

namespace GameServicesCloud.UI.Extensions;

public static class RefitDiExtensions {
    public static IHttpClientBuilder AddRefitBackendClient<T>(this WebAssemblyHostBuilder builder) where T : class {
        return builder.Services.AddRefitClient<T>()
            .AddHttpMessageHandler<AuthHeaderHandler>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.Configuration["Backend:AccountsBaseUrl"]!));
    }
}