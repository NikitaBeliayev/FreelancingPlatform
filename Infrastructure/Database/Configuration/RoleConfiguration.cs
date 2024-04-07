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
        builder.HasData(new List<Role>()
        {
            new Role(new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), 
                RoleName.BuildRoleNameWithoutValidation(RoleNameVariations.GetValue(RoleNameVariations.Admin).Value!).Value!, new List<User>()),
            new Role(new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), 
                RoleName.BuildRoleNameWithoutValidation(RoleNameVariations.GetValue(RoleNameVariations.Customer).Value!).Value!, new List<User>()),
            new Role(new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), 
                RoleName.BuildRoleNameWithoutValidation(RoleNameVariations.GetValue(RoleNameVariations.Implementer).Value!).Value!, new List<User>())
        });
        builder.Property(e => e.Name)
            .HasConversion(value => value.Value,
                value => RoleName.BuildRoleNameWithoutValidation(value).Value!);
        
        
    }
}