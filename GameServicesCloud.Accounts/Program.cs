using GameServicesCloud;
using GameServicesCloud.Accounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSharedServices(builder.Configuration.GetSection("Mail"), builder.Configuration.GetSection("MailTemplates"));

builder.Services.Configure<UserTokenOptions>(builder.Configuration.GetSection("UserTokens"));
builder.Services.Configure<AccountActivationOptions>(builder.Configuration.GetSection("AccountActivation"));
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

builder.Services.AddTransient<IUserTokenService, UserTokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    var corsOptions = builder.Configuration.GetSection("Cors").Get<CorsOptions>()!;

    options.AddPolicy("AllowSpecificOrigin",
        policy => {
            policy.WithOrigins(corsOptions.AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();