using Domain.Roles;
using Domain.Users;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasData(new List<Role>()
        {
            new Role(1, RoleNames.Admin, new List<User>()),
            new Role(2, RoleNames.Customer, new List<User>()),
            new Role(3, RoleNames.Implementer, new List<User>())
        });
        builder.Property(e => e.Name)
            .HasConversion(value => Enum.GetName(value.GetType(), value),
                value => (RoleNames)Enum.Parse(typeof(RoleNames), value));
        
        
    }
}