using GameServicesCloud;
using GameServicesCloud.Accounts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSharedServices(builder.Configuration.GetSection("Mail"));

builder.Services.Configure<UserTokenOptions>(builder.Configuration.GetSection("UserTokens"));
builder.Services.Configure<EmailVerificationOptions>(builder.Configuration.GetSection("EmailVerification"));

builder.Services.AddTransient<IUserTokenService, UserTokenService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();