using FreelancingPlatform.Controllers;
using Shared;
using Application.Users;
using Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

[TestFixture]
public class UserControllerTests
{
    private UserController _userController = null!;
    private Mock<ISender> _senderMock = null!;

    [SetUp]
    public void Setup()
    {
        _senderMock = new Mock<ISender>();
        _userController = new UserController(_senderMock.Object);
    }

    [Test]
    public async Task GetReturnsOkResultWhenUserExists()
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
        Assert.NotNull(result);
        Assert.That(result?.StatusCode, Is.EqualTo(200));
        Assert.That(result?.Value, Is.EqualTo(userDto));
    }

    [Test]
    public async Task GetReturnsBadRequestWhenUserDoesNotExist()
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
        Assert.NotNull(result);
        Assert.NotNull(receivedError);
        Assert.That(result?.StatusCode, Is.EqualTo(400));
        Assert.That(receivedError?.msg, Is.EqualTo("User not found"));
        Assert.That(receivedError?.Code, Is.EqualTo("400"));
    }

    [Test]
    public async Task PostReturnsOkResultWhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var userDto = new UserDTO();
        var commandResult = Result<UserDTO>.Success(userDto);
        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _userController.Post(userDto, CancellationToken.None) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.That(result?.StatusCode, Is.EqualTo(200));
        Assert.That(result?.Value, Is.EqualTo(userDto));
    }

    [Test]
    public async Task PostReturnsBadRequestWhenUserCreationFails()
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
        Assert.NotNull(result);
        Assert.NotNull(receivedError);
        Assert.That(result?.StatusCode, Is.EqualTo(400));
        Assert.That(receivedError?.msg, Is.EqualTo("User creation failed"));
        Assert.That(receivedError?.Code, Is.EqualTo("400"));
    }
}