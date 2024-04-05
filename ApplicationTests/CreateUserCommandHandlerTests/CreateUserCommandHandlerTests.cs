using Application.Abstraction;
using Application.Abstraction.Data;
using Application.Users;
using Application.Users.Create;
using AutoMapper;
using Domain.Repositories;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.UserDetails;
using Infrastructure.Automapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Legacy;

namespace ApplicationTests.CreateUserCommandHandlerTests;

[TestFixture]
public class CreateUserCommandHandlerTests
{
	private readonly Mock<IUserRepository> _userRepositoryMock = new();
	private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
	private readonly Mock<ILogger<CreateUserCommandHandler>> _logger = new();
	private Mapper _mapper;
	private readonly Mock<IHashProvider> _hashProvider = new Mock<IHashProvider>();
	private CreateUserCommandHandler _handler;

	[SetUp]
	public void SetUp()
	{
		MapperConfiguration configuration =
		new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles.AutoMapperProfile>());
		_mapper = new Mapper(configuration);
		_handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _logger.Object, _mapper,
			_hashProvider.Object);
	}
	[Test]
	public async Task Handle_WithValidCommand_ShouldCreateUserAndReturnUserDTO()
	{
		// Arrange
		var userGuid = Guid.NewGuid();
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d1";

		var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

		_hashProvider.Setup(provider => provider.GetHash(password))
			.Returns("4c0f384da99bb6a3db1b0098c3ef58a9a13dd3b524d9e9b623b90347e55afaf5");

		User user = new User(userGuid, Email.BuildEmail(email).Value!,
			Name.BuildName(firstName).Value!, Name.BuildName(lastName).Value!,
			Password.BuildPassword(password).Value!,
			new List<UserCommunicationChannel>(),
			new List<Role>());
		user.Password.Value = _hashProvider.Object.GetHash(password);

		_userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()))
			.ReturnsAsync(user);


		// Act
		var result = await _handler.Handle(command, new CancellationToken());

		// Assert
		ClassicAssert.IsTrue(result.IsSuccess);
		ClassicAssert.IsNotNull(result.Value);
		Assert.Multiple(() =>
		{
			Assert.That(result.Value!.FirstName, Is.EqualTo(firstName));
			Assert.That(result.Value!.LastName, Is.EqualTo(lastName));
			Assert.That(result.Value!.Email, Is.EqualTo(email));
			Assert.That(result.Value!.Password, Is.EqualTo(_hashProvider.Object.GetHash(password)));
		});

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Once);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
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

		var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
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

		var command = new CreateUserCommand(new UserDto(userGuid, email, firstName, lastName, password));

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
	}
}
