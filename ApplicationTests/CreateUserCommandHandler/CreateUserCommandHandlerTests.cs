using Application.Abstraction.Data;
using Application.Users;
using Application.Users.Create;
using AutoMapper;
using Domain.UserCommunicationChannels;
using Domain.Users.Errors;
using Domain.Users.Repositories;
using Domain.Users.UserDetails;
using Infrastructure.Automapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Legacy;

namespace ApplicationTests.CreateUserCommandHandlerTest;

[TestFixture]
public class CreateUserCommandHandlerTests
{
    [Test]
    public async Task Handle_WithValidCommand_ShouldCreateUserAndReturnUserDTO()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string email = "john.doe@example.com";
        string firstName = "John";
        string lastName = "Doe";
        string password = "epasswoR!d1";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserCommandHandler>>();
        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var _mapper = new Mapper(configuration);

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object, _mapper);

        var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

        userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new User(userGuid, EmailAddress.BuildEmail(email).Value!,
                                   Name.BuildName(firstName).Value!, Name.BuildName(lastName).Value!,
                                   Password.BuildPassword(password).Value!, new List<UserCommunicationChannel>()));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        ClassicAssert.IsTrue(result.IsSuccess);
        ClassicAssert.IsNotNull(result.Value);
        Assert.Multiple(() =>
        {
            Assert.That(result.Value!.FirstName, Is.EqualTo(firstName));
            Assert.That(result.Value!.LastName, Is.EqualTo(lastName));
            Assert.That(result.Value!.EmailAddress, Is.EqualTo(email));
            Assert.That(result.Value!.Password, Is.EqualTo(password));
        });

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidFirstName_ShouldReturnFailureResult()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string email = "john.doe@example.com";
        string firstName = null!;
        string lastName = "Doe";
        string password = "epasswoR!d1";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserCommandHandler>>();
        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var _mapper = new Mapper(configuration);

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object, _mapper);

        var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        ClassicAssert.IsFalse(result.IsSuccess);
        ClassicAssert.IsNull(result.Value);
        ClassicAssert.IsNotNull(result.Error);
        Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Handle_WithInvalidLastName_ShouldReturnFailureResult()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string email = "john.doe@example.com";
        string firstName = "Doe";
        string lastName = null!;
        string password = "epasswoR!d1";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserCommandHandler>>();
        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var _mapper = new Mapper(configuration);

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object, _mapper);

        var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        ClassicAssert.IsFalse(result.IsSuccess);
        ClassicAssert.IsNull(result.Value);
        ClassicAssert.IsNotNull(result.Error);
        Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}