using System.Linq.Expressions;
using System.Net;
using Application.Abstraction;
using Application.Users.Login;
using Application.Users.RequestDto;
using Domain.CommunicationChannels;
using Domain.Repositories;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.UserDetails;
using Moq;
using Shared;

namespace ApplicationTests.LoginUserCommandHandlerTests;

[TestFixture]
public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IJwtProvider> _jwtProviderMock = new();
    private readonly Mock<IHashProvider> _hashProviderMock = new();

    [TearDown]
    public void TearDown()
    {
        _userRepositoryMock.Reset();
        _jwtProviderMock.Reset();
        _hashProviderMock.Reset();
    }
    
    [Test]
    public async Task Handle_WithValidCommand_ShouldLoginAndReturnUserLoginResponseDto()
    {
        //Arrange
        var userLoginDto = new UserLoginDto()
        {
            Email = "john.doe@example.com",
            Password = "epasswoR!d1"
        };
        var emailAddress = Email.BuildEmail(userLoginDto.Email);
        var password = Password.BuildPassword(userLoginDto.Password);
        var command = new LoginUserCommand(userLoginDto);
        _hashProviderMock.Setup(hp => hp.GetHash(password.Value!.Value))
            .Returns("4c0f384da99bb6a3db1b0098c3ef58a9a13dd3b524d9e9b623b90347e55afaf5");
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = emailAddress.Value!,
            Password = password.Value!,
            FirstName = Name.BuildName("John").Value!,
            LastName = Name.BuildName("Doe").Value!,
            Objectives = {  },
            CommunicationChannels = new List<UserCommunicationChannel>
            {
                new UserCommunicationChannel
                {
                    CommunicationChannelId = (int)CommunicationChannelNameType.Email,
                    IsConfirmed = true
                }
            }
        };
        user.Password.Value = _hashProviderMock.Object.GetHash(password.Value!.Value);
        user.Roles.Add(new Role(2, RoleName.BuildRoleName(2).Value!, new List<User>()));
        
        _userRepositoryMock.Setup(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()))
            .ReturnsAsync(user);
        
        var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtProviderMock.Object, _hashProviderMock.Object);
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.Value!.Id, Is.EqualTo(user.Id));
        });

        _userRepositoryMock.Verify(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()), Times.Once);
        _hashProviderMock.Verify(hp => hp.GetHash(It.IsAny<string>()), Times.Exactly(2));
        _jwtProviderMock.Verify(jwt => jwt.GenerateCredentials(It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IEnumerable<string>>()), Times.Once);
    }
    
    
    [Test]
    public async Task Handle_WithInValidEmailAddressFormat_ShouldReturnEmailAddressFormatError()
    {
        //Arrange
        var userLoginDto = new UserLoginDto()
        {
            Email = "john.doeexample.com",
            Password = "epasswoR!d1"
        };
        var command = new LoginUserCommand(userLoginDto);
        var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtProviderMock.Object, _hashProviderMock.Object);
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Value, Is.Null);
            Assert.That(result.Error, Is.EqualTo(EmailAddressErrors.InvalidFormat));
        });

        _userRepositoryMock.Verify(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()), Times.Never);
        _hashProviderMock.Verify(hp => hp.GetHash(It.IsAny<string>()), Times.Never);
        _jwtProviderMock.Verify(jwt => jwt.GenerateCredentials(It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IEnumerable<string>>()), Times.Never);
    }
    
    [Test]
    public async Task Handle_WithExistingUser_ShouldReturnError()
    {
        var userLoginDto = new UserLoginDto()
        {
            Email = "john.doe@example.com",
            Password = "epasswoR!d1"
        };
        var command = new LoginUserCommand(userLoginDto);
        
        _userRepositoryMock.Setup(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()))
            .ReturnsAsync(null as User);
        
        var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtProviderMock.Object, _hashProviderMock.Object);
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error("User.LoginUserCommandHandler"
                , "There is no user with this email address", (int)HttpStatusCode.Unauthorized)));
            Assert.That(result.Value, Is.Null);
        });

        _userRepositoryMock.Verify(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()), Times.Once);
        _hashProviderMock.Verify(hp => hp.GetHash(It.IsAny<string>()), Times.Never);
        _jwtProviderMock.Verify(jwt => jwt.GenerateCredentials(It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IEnumerable<string>>()), Times.Never);
    }
    
    [Test]
    public async Task Handle_WithInvalidPasswordFormat_ShouldReturnPasswordFormatError()
    {
        var userLoginDto = new UserLoginDto()
        {
            Email = "john.doe@example.com",
            Password = "epasswo!d1"
        };
        
        var emailAddress = Email.BuildEmail(userLoginDto.Email);
        var password = Password.BuildPassword(userLoginDto.Password);
        var command = new LoginUserCommand(userLoginDto);
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = emailAddress.Value!,
            Password = password.Value!,
            FirstName = Name.BuildName("John").Value!,
            LastName = Name.BuildName("Doe").Value!,
            Objectives = {  },
            CommunicationChannels = new List<UserCommunicationChannel>
            {
                new UserCommunicationChannel
                {
                    CommunicationChannelId = (int)CommunicationChannelNameType.Email,
                    IsConfirmed = true
                }
            }
        };
        user.Roles.Add(new Role(2, RoleName.BuildRoleName(2).Value!, new List<User>()));
        
        _userRepositoryMock.Setup(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()))
            .ReturnsAsync(user);
        
        var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtProviderMock.Object, _hashProviderMock.Object);
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(PasswordErrors.Case));
            Assert.That(result.Value, Is.Null);
        });

        _userRepositoryMock.Verify(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()), Times.Once);
        _hashProviderMock.Verify(hp => hp.GetHash(It.IsAny<string>()), Times.Never);
        _jwtProviderMock.Verify(jwt => jwt.GenerateCredentials(It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IEnumerable<string>>()), Times.Never);
    }
    
    [Test]
    public async Task Handle_WithInvalidPassword_ShouldReturnNewError()
    {
        var userLoginDto = new UserLoginDto()
        {
            Email = "john.doe@example.com",
            Password = "epasswoR!d1"
        };
        var command = new LoginUserCommand(userLoginDto);
        
        var emailAddress = Email.BuildEmail(userLoginDto.Email);
        var password = Password.BuildPassword(userLoginDto.Password);
        _hashProviderMock.Setup(hp => hp.GetHash(It.IsAny<string>()))
            .Returns("4c0f384da99bb6a3db1b0098c3ef58a9a13dd3b524d9e9b623b90347e55afaf5");
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Email = emailAddress.Value!,
            Password = password.Value!,
            FirstName = Name.BuildName("John").Value!,
            LastName = Name.BuildName("Doe").Value!,
            Objectives = {  },
            CommunicationChannels = new List<UserCommunicationChannel>
            {
                new UserCommunicationChannel
                {
                    CommunicationChannelId = (int)CommunicationChannelNameType.Email,
                    IsConfirmed = true
                }
            }
        };
        user.Roles.Add(new Role(2, RoleName.BuildRoleName(2).Value!, new List<User>()));
        
        _userRepositoryMock.Setup(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()))
            .ReturnsAsync(user);
        
        var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _jwtProviderMock.Object, _hashProviderMock.Object);
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error("User.LoginUserCommandHandler",
                "Invalid password", (int)HttpStatusCode.Unauthorized)));
            Assert.That(result.Value, Is.Null);
        });

        _userRepositoryMock.Verify(rep => rep.GetByExpressionWithIncludesAsync(It.IsAny<Expression<Func<User, bool>>>(),
            It.IsAny<CancellationToken>(), It.IsAny<Expression<Func<User, object>>[]>()), Times.Once);
        _hashProviderMock.Verify(hp => hp.GetHash(It.IsAny<string>()), Times.Once);
        _jwtProviderMock.Verify(jwt => jwt.GenerateCredentials(It.IsAny<Guid>(), It.IsAny<string>(),
            It.IsAny<IEnumerable<string>>()), Times.Never);
    }
    
}