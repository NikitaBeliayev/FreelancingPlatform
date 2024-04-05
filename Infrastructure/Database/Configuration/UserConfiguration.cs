using Domain.Users;
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
                    value => Name.BuildNameWithoutValidation(value).Value!
                )
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => Name.BuildNameWithoutValidation(value).Value!
                )
                .IsRequired();

            builder.Property(p => p.Email)
                .HasConversion(
                    emailAddress => emailAddress.Value,
                    value => Email.BuildEmailWithoutValidation(value).Value!
                )
                .IsRequired();

            builder.Property(p => p.Password)
                .HasConversion(
                    password => password.Value,
                    value => Password.BuildPasswordWithoutValidation(value).Value!
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
                Email.BuildEmailWithoutValidation("testAdmin@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User1").Value!,
                Name.BuildNameWithoutValidation("Admin").Value!,
                Password.BuildPasswordWithoutValidation("9259de5682f80ba967a5263d420f44bb40a9267f9787d8034d597a69439e075f").Value!, null, null);

            User testCustomerUser = new User(new Guid("88755139-42b8-415b-84df-04c639d9b47a"),
                Email.BuildEmailWithoutValidation("testCustomer@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User2").Value!, 
                Name.BuildNameWithoutValidation("Customer").Value!,
                Password.BuildPasswordWithoutValidation("afed0e5dd16c3aa13c0913df9557fe7ff05129a2eb4bf9c54b2c68545aec63b1").Value!, null, null);

            User testImplementerUser = new User(new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"),
                Email.BuildEmailWithoutValidation("testImplementer@gmail.com").Value!,
                Name.BuildNameWithoutValidation("User3").Value!,
                Name.BuildNameWithoutValidation("Implementer").Value!,
                Password.BuildPasswordWithoutValidation("d7b810563cf203ede3043fde799c9705b9ca66635c68cf0465ac3259200f59fe").Value!, null, null);
        

        builder.HasData(new List<User>()
            {
                testAdminUser,
                testCustomerUser,
                testImplementerUser
            });
        }
    }
}
