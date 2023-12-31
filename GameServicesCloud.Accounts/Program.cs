using GameServicesCloud;
using GameServicesCloud.Accounts;
using GameServicesCloud.Accounts.StartupSetup;
using GameServicesCloud.Extensions;
using GameServicesCloud.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSharedServices();
builder.Services.AddMailServices(builder.Configuration.GetSection("Mail"), builder.Configuration.GetSection("MailTemplates"));

builder.Services.Configure<UserTokenOptions>(builder.Configuration.GetSection("UserTokens"));
builder.Services.Configure<AccountActivationOptions>(builder.Configuration.GetSection("AccountActivation"));
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<RootAdminOptions>(builder.Configuration.GetSection("RootAdmin"));

builder.Services.AddTransient<IUserTokenService, UserTokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IClaimService, ClaimService>();
builder.Services.AddTransient<IUserClaimService, UserClaimService>();
builder.Services.AddSingleton<ControllerClaimProviderService>();

builder.Services.AddControllers(options => options.Filters.Add<AuthorizationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddDefaultAuthentication(builder.Configuration.GetSection("JWT"));
builder.Services.AddAuthorization();
builder.AddDefaultCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSharedServices(options => {
    options.ClaimPrefix = "account";
});

await app.Setup();

app.Run();