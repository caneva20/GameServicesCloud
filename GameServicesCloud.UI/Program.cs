using GameServicesCloud.UI;
using GameServicesCloud.UI.HttpsClients;
using GameServicesCloud.UI.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(services => services.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<AccountsHttpClient>(client => client.BaseAddress = new Uri(builder.Configuration["Backend:AccountsBaseUrl"]!));

await builder.Build().RunAsync();