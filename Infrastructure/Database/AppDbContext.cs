using Microsoft.EntityFrameworkCore;
using Domain.UserCommunicationChannels;
using Domain.CommunicationChannels;
using Domain.Objectives;
using Domain.Roles;
using Domain.Statuses;
using Domain.Types;
using Domain.Users.UserDetails;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<CommunicationChannel> CommunicationChannels => Set<CommunicationChannel>();
        public DbSet<UserCommunicationChannel> UserCommunicationChannels => Set<UserCommunicationChannel>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<Objective> Objectives => Set<Objective>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
