using System.Reflection;
using Domain.Roles;
using Domain.Users;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);
        ConstructorInfo? privateConstructor = typeof(RoleName).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, [typeof(RoleNameType)]);
        
        if (privateConstructor is null)
        {
            throw new NullReferenceException("Constructor not found");
        }
        
        builder.HasData(new List<Role>()
        {
            new Role(1, (RoleName)privateConstructor.Invoke([RoleNameType.Admin]), new List<User>()),
            new Role(2, (RoleName)privateConstructor.Invoke([RoleNameType.Customer]), new List<User>()),
            new Role(3, (RoleName)privateConstructor.Invoke([RoleNameType.Implementer]), new List<User>())
        });
        builder.Property(e => e.Name)
            .HasConversion(value => value.Value,
                value => RoleName.BuildRoleNameWithoutValidation(
                    (int)(RoleNameType)Enum.Parse(typeof(RoleNameType), value)).Value!);
        
        
    }
}