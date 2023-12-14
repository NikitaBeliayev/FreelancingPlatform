namespace DomainTests.Name;

[TestFixture]
public class NameTests
{
    [Test]
    public void BuildName_WithValidValue_ShouldCreateName()
    {
        // Arrange
        string validValue = "John Doe";

        // Act
        var result = Domain.Users.Name.BuildName(validValue);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
        Assert.That(result.Value?.Value, Is.EqualTo(validValue));
    }

    [Test]
    public void BuildName_WithNullOrEmptyValue_ShouldFail()
    {
        // Arrange
        string invalidValue = null!;

        // Act
        var result = Domain.Users.Name.BuildName(invalidValue);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        Assert.That(result.Error, Is.EqualTo(Domain.Users.NameErrors.NullOrEmpty));
    }
}