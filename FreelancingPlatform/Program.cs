using Application.Abstraction.Data;
using Application.Users;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Repositories;
using Scrutor;
using Serilog;
using System.Reflection;
using FreelancingPlatform.OptionsConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static System.Net.Mime.MediaTypeNames;
using AutoMapper;
using Infrastructure.Automapper;
using FreelancingPlatform.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https:git //aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure())
            );
//builder.Services.AddTransient<UserDTO>();
builder
    .Services
    .Scan(
        x => x.FromAssemblies(
                Infrastructure.MetaData.AssemblyInfo.Assembly,
                Persistence.MetaData.AssemblyInfo.Assembly
            )
        .AddClasses(false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());

//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder
    .Services.AddMediatR(
        x => x.RegisterServicesFromAssembly(Application.MetaData.AssemblyInfo.Assembly));

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseGlobalExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.Migrate();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
