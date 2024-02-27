using NUnit.Framework.Legacy;
using Shared;

namespace SharedTests.ErrorTests;

[TestFixture]
public class ErrorTests
{
    [Test]
    public void Handle_WithError_ShouldReturnCorrectErrorObject()
    {
        // Arrange
        string code = "testCode";
        string message = "testMessage";
        int statusCode = 404;

        // Act
        var result = new Error(code, message, statusCode);
        
        // Assert
        Assert.Multiple(() =>
        {
            ClassicAssert.IsNotNull(result);
            Assert.That(result.Message, Is.EqualTo(message));
            Assert.That(result.Code, Is.EqualTo(code));
            Assert.That(result.StatusCode, Is.EqualTo(statusCode));
        });
    }
    
    [Test]
    public void Handle_WithNoneError_ShouldReturnCorrectErrorObject()
    {
        // Act
        var result = Error.None;
        
        // Assert
        Assert.Multiple(() =>
        {
            ClassicAssert.IsNotNull(result);
            Assert.That(result.Message, Is.EqualTo(string.Empty));
            Assert.That(result.Code, Is.EqualTo(string.Empty));
            Assert.That(result.StatusCode, Is.EqualTo(default(int)));
        });
    }
    
    [Test]
    public void Handle_WithToResultMethod_ShouldReturnFailureResultObject()
    {
        //Arrange
        string code = "testCode";
        string message = "testMessage";
        int statusCode = 404;
        Error error = new Error(code, message, statusCode);
        // Act
        var result = error.ToResult();
        
        // Assert
        Assert.Multiple(() =>
        {
            ClassicAssert.IsNotNull(result);
            Assert.That(result.IsSuccess, Is.EqualTo(false));
            Assert.That(result.Error.StatusCode, Is.EqualTo(statusCode));
            Assert.That(result.Error.Message, Is.EqualTo(message));
            Assert.That(result.Error.Code, Is.EqualTo(code));
        });
    }
}