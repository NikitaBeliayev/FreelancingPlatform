using Application.Roles;
using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using Domain.Roles;
using Domain.Users.Errors;
using Domain.Users.UserDetails;
using FreelancingPlatform.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework.Legacy;
using Shared;
using System.Security.Claims;
using FreelancingPlatform.Controllers;

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
    public async Task CreateUser_ReturnsOkResult_ForAdminUser()
    {
        var userId = Guid.NewGuid();
        string email = "john.doe@example.com";
        string firstName = "John";
        string lastName = "Doe";
        string password = "epasswoR!d1";
        var userDto = new UserDto(userId, email, firstName, lastName, password);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Role, nameof(RoleNameType.Customer))
        }));

        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserDto>.Success(userDto));

        _userController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var result = await _userController.CreateUser(userDto, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.Data, Is.EqualTo(userDto));
        });

        _senderMock.Verify(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }



    [Test]
    public async Task AdminControllerMethod_ReturnsOkResult_ForAdminUser()
    {
        var userId = Guid.NewGuid();
        var userDto = new UserDto();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Role, nameof(RoleNameType.Admin))
        }));

        var queryResult = Result<UserDto>.Success(userDto);

        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        _userController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.Data, Is.EqualTo(userDto));
        });
    }

    [Test]
    public async Task AdminControllerMethod_ReturnsBadRequest_ForNonAdminUser()
    {
        var userId = Guid.NewGuid();
        var expectedError = UserErrors.NotFound(userId);
        var queryResult = Result<UserDto>.Failure(null, expectedError);
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        new Claim(ClaimTypes.Role, nameof(RoleNameType.Customer))
        }));

        _userController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        Assert.That(result, Is.Not.Null);
        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response?.StatusCode, Is.EqualTo(expectedError.StatusCode));
            Assert.That(response?.StatusCode, Is.EqualTo(expectedError.StatusCode));
        });
    }


    [Test]
    public async Task Get_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new UserDto();
        var queryResult = Result<UserDto>.Success(userDto);
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(queryResult);

        // Act
        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        // Assert
        ClassicAssert.NotNull(result);
        Assert.Multiple(() =>
        {
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            Assert.That(response?.Data, Is.EqualTo(userDto));
        });
	}

    [Test]
    public async Task Get_ReturnsBadRequest_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedError = UserErrors.NotFound(userId);
        var queryResult = Result<UserDto>.Failure(null, expectedError);
        _senderMock.Setup(x => x.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(queryResult);

        // Act
        var result = await _userController.Get(userId, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        // Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.NotNull(response);
		Assert.Multiple(() =>
		{
			Assert.That(response?.StatusCode, Is.EqualTo(expectedError.StatusCode));
			Assert.That(response?.StatusCode, Is.EqualTo(expectedError.StatusCode));
		});
	}

    [Test]
    public async Task Post_ReturnsOkResult_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var userDto = new UserDto();
        var commandResult = Result<UserDto>.Success(userDto);
        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _userController.Post(userDto, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        // Assert
        ClassicAssert.NotNull(result);
        Assert.Multiple(() =>
		{
			Assert.That(result?.StatusCode, Is.EqualTo(200));
			Assert.That(response?.Data, Is.EqualTo(userDto));
		});
	}

    [Test]
    public async Task Post_ReturnsBadRequest_WhenUserCreationFails()
    {
        // Arrange
        var userDto = new UserDto();
        var expectedError = new Error("Failed", "User creation failed", 400);
        var commandResult = Result<UserDto>.Failure(null, expectedError);
        _senderMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);

        // Act
        var result = await _userController.Post(userDto, CancellationToken.None) as ObjectResult;
        var response = ApiResponse<UserDto>.FromResult((Result<UserDto>)result.Value, result.StatusCode.Value);

        // Assert
        ClassicAssert.NotNull(result);
        ClassicAssert.NotNull(response);
		Assert.Multiple(() =>
		{
			Assert.That(response?.StatusCode, Is.EqualTo(400));
			Assert.That(response?.Message, Is.EqualTo("User creation failed"));
		});
	}
}