using Blazored.LocalStorage;
using GameServicesCloud.UI;
using GameServicesCloud.UI.Clients;
using GameServicesCloud.UI.Clients.Accounts;
using GameServicesCloud.UI.Clients.Persistence;
using GameServicesCloud.UI.Clients.Ranking;
using GameServicesCloud.UI.Extensions;
using GameServicesCloud.UI.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(services => services.GetRequiredService<AuthStateProvider>());
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.AddRefitBackendClient<IAuthApi>("Accounts", false);
builder.AddRefitBackendClient<IUserApi>("Accounts");
builder.AddRefitBackendClient<IClaimApi>("Accounts");
builder.AddRefitBackendClient<IUserClaimApi>("Accounts");
builder.AddRefitBackendClient<IUserDataApi>("Persistence");
builder.AddRefitBackendClient<ILeaderboardApi>("Leaderboard");

builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();