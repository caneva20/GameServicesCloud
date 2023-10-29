using GameServicesCloud;
using GameServicesCloud.Extensions;
using GameServicesCloud.Filters;
using GameServicesCloud.Ranking;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddData(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSharedServices();

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
    options.ClaimPrefix = "ranking";
});

app.Run();