using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Legacy;
using Shared;

namespace Tests.UserController;

[TestFixture]
public class UserControllerTests
{
    private FreelancingPlatform.Controllers.UserController _userController = null!;
    private Mock<ISender> _senderMock = null!;

    [SetUp]
    public void Setup()
    {
        _senderMock = new Mock<ISender>();
        _userController = new FreelancingPlatform.Controllers.UserController(_senderMock.Object);
    }

    [Test]
    public async Task Get_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new UserDTO();
        var queryResult = Result<UserDTO>.Success(userDto);
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(queryResult);

        // Act
        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;

        // Assert
        ClassicAssert.NotNull(result);
        Assert.That(result?.StatusCode, Is.EqualTo(200));
        Assert.That(result?.Value, Is.EqualTo(userDto));
    }

    [Test]
    public async Task Get_ReturnsBadRequest_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedError = new Error("400", "User not found");
        var queryResult = Result<UserDTO>.Failure(null, expectedError);
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;
        var receivedError = result?.Value as Error;

        // Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.NotNull(receivedError);
        Assert.That(result?.StatusCode, Is.EqualTo(400));
        Assert.That(receivedError?.msg, Is.EqualTo("User not found"));
        Assert.That(receivedError?.Code, Is.EqualTo("400"));
    }

    [Test]
    public async Task Post_ReturnsOkResult_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var userDto = new UserDTO();
        var commandResult = Result<UserDTO>.Success(userDto);
        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _userController.Post(userDto, CancellationToken.None) as ObjectResult;

        // Assert
        ClassicAssert.NotNull(result);
        Assert.That(result?.StatusCode, Is.EqualTo(200));
        Assert.That(result?.Value, Is.EqualTo(userDto));
    }

    [Test]
    public async Task Post_ReturnsBadRequest_WhenUserCreationFails()
    {
        // Arrange
        var userDto = new UserDTO();
        var expectedError = new Error("400", "User creation failed");
        var commandResult = Result<UserDTO>.Failure(null, expectedError);
        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _userController.Post(userDto, CancellationToken.None) as ObjectResult;
        var receivedError = result?.Value as Error;

        // Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.NotNull(receivedError);
        Assert.That(result?.StatusCode, Is.EqualTo(400));
        Assert.That(receivedError?.msg, Is.EqualTo("User creation failed"));
        Assert.That(receivedError?.Code, Is.EqualTo("400"));
    }
}