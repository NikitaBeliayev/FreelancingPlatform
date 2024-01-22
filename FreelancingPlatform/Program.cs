using Microsoft.EntityFrameworkCore;
using Scrutor;
using Serilog;
using FreelancingPlatform.OptionsConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Automapper;
using FreelancingPlatform.Middleware;
using FreelancingPlatform.OptionsValidation;
using Infrastructure.EmailProvider;
using Microsoft.Extensions.Options;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Infrastructure.DatabaseConfiguration;
using Infrastructure.HashProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<EmailProviderOptionsSetup>();
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
builder.Services.ConfigureOptions<HashOptionsSetup>();

builder.Services.AddSingleton<IValidateOptions
    <EmailOptions>, EmailOptionsValidation>();
builder.Services.AddSingleton<IValidateOptions
    <JwtOptions>, JwtOptionsValidation>();
builder.Services.AddSingleton<IValidateOptions
    <DatabaseOptions>, DatabaseOptionsValidation>();
builder.Services.AddSingleton<IValidateOptions
    <HashOptions>, HashOptionsValidation>();


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https:git //aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure())
            );
builder
    .Services
    .Scan(
        x => x.FromAssemblies(
                Infrastructure.MetaData.AssemblyInfo.Assembly
            )
        .AddClasses(false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder
    .Services.AddMediatR(
        x => x.RegisterServicesFromAssembly(Application.MetaData.AssemblyInfo.Assembly));

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

if (builder.Configuration.GetSection("Database").GetValue<bool>("ApplyAutomaticMigrations"))
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        appDbContext.Database.Migrate();
    }
}

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();