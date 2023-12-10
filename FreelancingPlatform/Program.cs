using Application.Abstraction.Data;
using Application.Users;
using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Repositories;
using Scrutor;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
