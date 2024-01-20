using Domain.Users;
using Domain.Users.UserDetails;
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

            builder.Property(p => p.Email)
                .HasConversion(
                    emailAddress => emailAddress.Value,
                    value => EmailAddress.BuildEmail(value).Value!
                )
                .IsRequired();

            builder.Property(p => p.Password)
                .HasConversion(
                    password => password.Value,
                    value => Password.BuildHashed(value).Value!
                )
                .IsRequired();

            builder.HasMany(e => e.CommunicationChannels)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.HasMany(e => e.Roles)
                .WithMany(e => e.Users);
        }
    }
}
