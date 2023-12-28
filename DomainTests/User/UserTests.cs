namespace DomainTests.User;

[TestFixture]
public class UserTests
{
    [Test]
    public void Constructor_WithValidArguments_ShouldCreateUser()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Domain.Users.EmailAddress email = Domain.Users.EmailAddress.BuildEmail("john.doe@example.com").Value!;
        Domain.Users.Name firstName = Domain.Users.Name.BuildName("John").Value!;
        Domain.Users.Name lastName = Domain.Users.Name.BuildName("Doe").Value!;
        Domain.Users.Password password = Domain.Users.Password.BuildPassword("Password123").Value!;

        // Act
        var user = new Domain.Users.User(userId, email, firstName, lastName, password);

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