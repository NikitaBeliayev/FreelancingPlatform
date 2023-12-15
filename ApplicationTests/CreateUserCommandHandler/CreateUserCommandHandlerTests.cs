using Application.Abstraction.Data;
using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using Domain.Users;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApplicationTests.CreateUserCommandHandler;

[TestFixture]
public class CreateUserCommandHandlerTests
{
    [Test]
    public async Task Handle_WithValidCommand_ShouldCreateUserAndReturnUserDTO()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string firstName = "John";
        string lastName = "Doe";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserQueryHandler>>();

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object);

        var command = new CreateUserCommand(new UserDTO(userGuid, firstName, lastName));

        userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync(new User(Guid.NewGuid(), Name.BuildName(firstName).Value!, Name.BuildName(lastName).Value!));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Value);
        Assert.That(result.Value!.FirstName, Is.EqualTo(firstName));
        Assert.That(result.Value!.LastName, Is.EqualTo(lastName));

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidFirstName_ShouldReturnFailureResult()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string firstName = null!;
        string lastName = "Doe";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserQueryHandler>>();

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object);

        var command = new CreateUserCommand(new UserDTO(userGuid, firstName, lastName));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        Assert.IsNotNull(result.Error);
        Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Handle_WithInvalidLastName_ShouldReturnFailureResult()
    {
        // Arrange
        var userGuid = Guid.NewGuid();
        string firstName = null!;
        string lastName = "Doe";
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var logger = new Mock<ILogger<CreateUserQueryHandler>>();

        var handler = new Application.Users.Create.CreateUserCommandHandler(userRepositoryMock.Object, unitOfWorkMock.Object, logger.Object);

        var command = new CreateUserCommand(new UserDTO(userGuid, firstName, lastName));
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsNull(result.Value);
        Assert.IsNotNull(result.Error);
        Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

        userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}