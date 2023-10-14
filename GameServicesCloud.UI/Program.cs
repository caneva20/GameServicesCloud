using Blazored.LocalStorage;
using GameServicesCloud.UI;
using GameServicesCloud.UI.Clients;
using GameServicesCloud.UI.Services.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;

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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<AccountsHttpClient>(client => client.BaseAddress = new Uri(builder.Configuration["Backend:AccountsBaseUrl"]!));
builder.Services.AddRefitClient<IUserApi>()
    .AddHttpMessageHandler<AuthHeaderHandler>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(builder.Configuration["Backend:AccountsBaseUrl"]!));

builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();