using Application.Abstraction;
using Application.Abstraction.Data;
using Application.Models;
using Application.Models.Email;
using Application.Users.Create;
using Application.Users.Register;
using Application.Users.RequestDto;
using AutoMapper;
using Domain.Repositories;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users;
using Domain.Users.Errors;
using Domain.Users.UserDetails;
using Infrastructure.Automapper;
using Infrastructure.EmailProvider;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Legacy;

namespace ApplicationTests.RegisterUserCommandHandlerTests;

[TestFixture]
public class RegisterUserCommandHandlerTests
{
	private readonly Mock<IUserRepository> _userRepositoryMock = new();
	private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
	private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock = new();
	private readonly Mock<IEmailProvider> _emailProviderMock = new();
	private readonly Mock<IRoleRepository> _roleRepositoryMock = new();
	private readonly Mock<ICommunicationChannelRepository> _communicationChannelRepositoryMock = new();
	private readonly Mock<IUserCommunicationChannelRepository> _userCommunicationChannelRepositoryMock = new();
	private Mapper _mapper;
	private readonly Mock<IHashProvider> _hashProviderMock = new();
	private RegisterUserCommandHandler _handler;

	[SetUp]
	public void SetUp()
	{
		MapperConfiguration configuration =
			new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles.AutoMapperProfile>());
		_mapper = new Mapper(configuration);
		_handler = new RegisterUserCommandHandler(_userRepositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object, _mapper, _emailProviderMock.Object,
			_roleRepositoryMock.Object, _communicationChannelRepositoryMock.Object, _userCommunicationChannelRepositoryMock.Object, _hashProviderMock.Object);
	}

	[Test]
	public async Task Handle_WithValidCommand_ShouldRegisterUserAndReturnUserRegistrationResponseDTO()
	{
		//Arrange
		Guid guid = new Guid();
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d1";

		Role customerRole = new Role()
		{
			Id = 2,
			Name = RoleName.BuildRoleName(2).Value!
		};

		Role implementerRole = new Role()
		{
			Id = 3,
			Name = RoleName.BuildRoleName(3).Value!
		};

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		_hashProviderMock.Setup(provider => provider.GetHash("epasswoR!d1"))
			.Returns("4c0f384da99bb6a3db1b0098c3ef58a9a13dd3b524d9e9b623b90347e55afaf5");

		User user = new User(guid, Email.BuildEmail(email).Value!,
			Name.BuildName(firstName).Value!, Name.BuildName(lastName).Value!,
			Password.BuildPassword(password).Value!,
			new List<UserCommunicationChannel>(),
			new List<Role>()
			{
				customerRole, implementerRole
			});

		_userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()))
			.ReturnsAsync(user);

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsTrue(result.IsSuccess);
		ClassicAssert.IsNotNull(result.Value);
		Assert.That(result.Value!.Id, Is.EqualTo(guid));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Once);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Once);
	}

	[Test]
	public async Task Handle_WithInvalidFirstName_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = null!;
		string lastName = "Doe";
		string password = "epasswoR!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));
		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithInvalidLastName_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = null!;
		string password = "epasswoR!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});


		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(NameErrors.NullOrEmpty));
		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithNullEmail_ShouldReturnFailureResult()
	{
		//Arrange
		string email = null!;
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(EmailAddressErrors.InvalidFormat));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithInvalidEmail_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doeexample.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(EmailAddressErrors.InvalidFormat));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never); _emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithPasswordWithoutDigits_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(PasswordErrors.Digit));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithSpacePassword_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epas swoR!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(PasswordErrors.Space));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithUpperCasePassword_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswor!d1";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(PasswordErrors.Case));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}
	[Test]
	public async Task Handle_WithLongPassword_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epasswoR!d1333333333";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(PasswordErrors.Length));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}

	[Test]
	public async Task Handle_WithShortPassword_ShouldReturnFailureResult()
	{
		//Arrange
		string email = "john.doe@example.com";
		string firstName = "John";
		string lastName = "Doe";
		string password = "epa";

		var command = new RegisterUserCommand(new UserRegistrationDto()
		{
			Email = email,
			FirstName = firstName,
			LastName = lastName,
			Password = password,
			Role = 2
		});

		//Act
		var result = await _handler.Handle(command, new CancellationToken());

		//Assert
		ClassicAssert.IsFalse(result.IsSuccess);
		ClassicAssert.IsNull(result.Value);
		ClassicAssert.IsNotNull(result.Error);
		Assert.That(result.Error, Is.EqualTo(PasswordErrors.Length));

		_userRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<User>(), new CancellationToken()), Times.Never);
		_unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		_emailProviderMock.Verify(emailProvider => emailProvider.SendAsync(It.IsAny<EmailMessageComposerModel>(),
			It.IsAny<CancellationToken>()), Times.Never);
	}
}
