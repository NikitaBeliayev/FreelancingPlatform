using Application.Abstraction;
using Domain.Users.UserDetails;
using Infrastructure.Database;
using Infrastructure.HashProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();

var contextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
var context = new AppDbContext(contextOptionsBuilder.Options);

HashOptions hashOptions = new HashOptions() { secretPepper = configuration.GetSection("Hashing").GetSection("SecretPepper").ToString() };
IHashProvider hashProvider = new HashProvider(Options.Create(hashOptions));

var users = context.Users.ToList();

foreach (User user in users)
{
    user.Password.Value = hashProvider.GetHash(user.Password.Value);
}

context.SaveChanges();