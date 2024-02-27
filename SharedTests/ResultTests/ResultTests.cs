using System.Security.Claims;
using NUnit.Framework.Legacy;
using Shared;
namespace SharedTests.ResultTests;

[TestFixture]
public class ResultTests
{
    [Test]
    public void Handle_WithResult_ShouldReturnSuccessResultObject()
    {
        //Act
        var result = Result.Success();
        
        Assert.Multiple(() =>
        {
            ClassicAssert.IsNotNull(result);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Error, Is.EqualTo(Error.None));
        });
    }
    
    [Test]
    public void Handle_WithResult_ShouldReturnFailureResultObject()
    {
        //Arrange
        Error error = new Error("testStatusCode", "testMessage", 404);
        //Act
        var result = Result.Failure(error);
        
        Assert.Multiple(() =>
        {
            ClassicAssert.IsNotNull(result);
            Assert.That(result.IsSuccess, Is.False);
            ClassicAssert.IsNotNull(result.Error);
            Assert.That(result.Error, Is.EqualTo(error));
        });
    }
}