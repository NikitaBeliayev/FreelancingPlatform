using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
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
                .WithMany(e => e.Users)
                .UsingEntity(j => j.HasData(new object[]
                {
                    new { RolesId = 1, UsersId = new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca")},
                    new { RolesId = 2, UsersId = new Guid("88755139-42b8-415b-84df-04c639d9b47a")},
                    new { RolesId = 3, UsersId = new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5")}
                }));

            User testAdminUser = new User(
                new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"),
                EmailAddress.BuildEmailWithoutValidation("testAdmin@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User1").Value!,
                Name.BuildNameWithoutValidation("Admin").Value!,
                Password.BuildHashedWithoutValidation("3c7e2f8d6920bc72da2b24e9d47ca302ce56b6a047ff70d352294bbaf2bf3054").Value!, null, null);

            User testCustomerUser = new User(new Guid("88755139-42b8-415b-84df-04c639d9b47a"),
                EmailAddress.BuildEmailWithoutValidation("testCustomer@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User2").Value!, 
                Name.BuildNameWithoutValidation("Customer").Value!,
                Password.BuildHashedWithoutValidation("005ecd48c62dfa2d5d2824958a2ac400fbd82b8e94a8fc5f9fcc9c091a1c7d39").Value!, null, null);

            User testImplementerUser = new User(new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"),
                EmailAddress.BuildEmailWithoutValidation("testImplementer@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User3").Value!,
                Name.BuildNameWithoutValidation("Implementer").Value!,
                Password.BuildHashedWithoutValidation("f36711e3bac2be05f53c12f7722279162bd1ecb945a331db5cb1bed89e2d298f").Value!, null, null);
        

        builder.HasData(new List<User>()
            {
                testAdminUser,
                testCustomerUser,
                testImplementerUser
            });
        }
    }
}
