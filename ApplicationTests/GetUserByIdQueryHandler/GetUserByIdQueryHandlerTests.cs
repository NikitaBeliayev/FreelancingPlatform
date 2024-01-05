using Application.Abstraction.Data;
using Application.Users.GetById;
using AutoMapper;
using Domain.Users;
using Infrastructure.Automapper;
using Moq;
using NUnit.Framework.Legacy;

namespace ApplicationTests.GetUserByIdQueryHandler;

[TestFixture]
public class CreateUserQueryHandlerTests
{
    [Test]
    public async Task Handle_WithValidUserId_ShouldReturnUserDTO()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var _mapper = new Mapper(configuration);


        var handler = new Application.Users.GetById.GetUserByIdQueryHandler(userRepositoryMock.Object, unitOfWorkMock.Object, _mapper);

        var userId = Guid.NewGuid();
        var query = new GetUserByIdQuery(userId);

        string firstName = "John";
        string lastName = "Doe";

        var user = new User(userId, EmailAddress.BuildEmail("john.doe@example.com").Value!, Name.BuildName(firstName).Value!, Name.BuildName(lastName).Value!, Password.BuildPassword("epasswoR!d1").Value!);
        userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId, CancellationToken.None))
            .ReturnsAsync(user);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        ClassicAssert.IsTrue(result.IsSuccess);
        ClassicAssert.IsNotNull(result.Value);
        Assert.Multiple(() =>
        {
            Assert.That(result.Value?.Id, Is.EqualTo(userId));
            Assert.That(result.Value?.FirstName, Is.EqualTo(firstName));
            Assert.That(result.Value?.LastName, Is.EqualTo(lastName));
        });

        userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId, CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidUserId_ShouldReturnNullUserDTO()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var myProfile = new AutoMapperProfiles.AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var _mapper = new Mapper(configuration);

        var handler = new Application.Users.GetById.GetUserByIdQueryHandler(userRepositoryMock.Object, unitOfWorkMock.Object, _mapper);

        var invalidUserId = Guid.NewGuid();
        var query = new GetUserByIdQuery(invalidUserId);

        userRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidUserId, CancellationToken.None))
            .ReturnsAsync((User)null!);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        ClassicAssert.IsTrue(result.IsSuccess);
        ClassicAssert.IsNull(result.Value);

        userRepositoryMock.Verify(repo => repo.GetByIdAsync(invalidUserId, CancellationToken.None), Times.Once);
    }
}