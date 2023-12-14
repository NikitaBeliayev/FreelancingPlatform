namespace DomainTests.User;

[TestFixture]
public class UserTests
{
    [Test]
    public void Constructor_WithValidArguments_ShouldCreateUser()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        Domain.Users.Name firstName = Domain.Users.Name.BuildName("John").Value!;
        Domain.Users.Name lastName = Domain.Users.Name.BuildName("Doe").Value!;

        // Act
        var user = new Domain.Users.User(userId, firstName, lastName);

        // Assert
        Assert.That(user.Id, Is.EqualTo(userId));
        Assert.That(user.FirstName, Is.EqualTo(firstName));
        Assert.That(user.LastName, Is.EqualTo(lastName));
    }
}