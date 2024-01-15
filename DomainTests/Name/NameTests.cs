using Domain.Users.Errors;
using NUnit.Framework.Legacy;

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
        var result = Domain.Users.UserDetails.Name.BuildName(validValue);

        // Assert
        ClassicAssert.IsTrue(result.IsSuccess);
        ClassicAssert.IsNotNull(result.Value);
        Assert.That(result.Value?.Value, Is.EqualTo(validValue));
    }

    [Test]
    public void BuildName_WithNullOrEmptyValue_ShouldFail()
    {
        // Arrange
        string invalidValue = null!;

        // Act
        var result = Domain.Users.UserDetails.Name.BuildName(invalidValue);

        // Assert
        ClassicAssert.IsFalse(result.IsSuccess);
        ClassicAssert.IsNull(result.Value);
        Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));
    }
}