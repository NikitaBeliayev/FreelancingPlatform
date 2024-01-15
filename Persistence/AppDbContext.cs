using Microsoft.EntityFrameworkCore;
using Domain.UserCommunicationChannels;
using Domain.CommunicationChannels;
using Domain.Users.UserDetails;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<CommunicationChannel> CommunicationChannels => Set<CommunicationChannel>();
        public DbSet<UserCommunicationChannel> UserCommunicationChannels => Set<UserCommunicationChannel>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
