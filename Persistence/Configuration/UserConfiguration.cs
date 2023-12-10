using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.FirstName)
                .HasConversion(
                    firstName => firstName.Value,
                    value => Name.BuildName(value).Value!
                )
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => Name.BuildName(value).Value!
                )
                .IsRequired();
        }
    }
}
