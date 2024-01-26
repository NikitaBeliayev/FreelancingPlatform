using Microsoft.EntityFrameworkCore;
using Domain.UserCommunicationChannels;
using Domain.CommunicationChannels;
using Domain.Roles;
using Domain.Users.UserDetails;
using Domain.Objectives.ObjectiveStatus;

namespace Infrastructure.Database
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
        public DbSet<Role> Role => Set<Role>();
        public DbSet<ObjectiveStatus> ObjectiveStatuses => Set<ObjectiveStatus>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
