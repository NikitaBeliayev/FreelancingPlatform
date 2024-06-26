using Domain.Objectives;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users;
using Domain.Users.UserDetails;

namespace DomainTests.User;

[TestFixture]
public class UserTests
{
    [Test]
    public void Constructor_WithValidArguments_ShouldCreateUser()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Email email = Email.BuildEmail("john.doe@example.com").Value!;
        Domain.Users.UserDetails.Name firstName = Domain.Users.UserDetails.Name.BuildName("John").Value!;
        Domain.Users.UserDetails.Name lastName = Domain.Users.UserDetails.Name.BuildName("Doe").Value!;
        Password password = Password.BuildPassword("Password123").Value!;

        // Act
        var user = new Domain.Users.User(userId, email, firstName, lastName, password, 
            new List<UserCommunicationChannel>(),
            new List<Role>(), new List<Objective>(), new List<Objective>());

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(user.Id, Is.EqualTo(userId));
            Assert.That(user.Email, Is.EqualTo(email));
            Assert.That(user.FirstName, Is.EqualTo(firstName));
            Assert.That(user.LastName, Is.EqualTo(lastName));
            Assert.That(user.Password, Is.EqualTo(password));
        });
	}
}