using FreelancingPlatform.Middleware;
using FreelancingPlatform.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();
builder.Services.AddOptionsValidation();
builder.Services.AddCustomAutoMapper();
builder.Services.AddCustomCors();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomRequestTimeouts(builder.Configuration);
builder.Services.AddCustomDatabase(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddCustomControllers();

builder.Host.AddCustomLogging();

var app = builder.Build();

app.UseGlobalWrapperMiddleware();
app.UseCustomSwagger();
app.ApplyDatabaseMigrations();

app.UseCors();
app.UseCustomRequestLogging();
app.UseRequestTimeouts();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
