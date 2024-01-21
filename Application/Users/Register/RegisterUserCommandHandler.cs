using System.Net;
using Application.Abstraction;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Users.Create;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.CommunicationChannels;
using Domain.CommunicationChannels.Repositories;
using Domain.Roles;
using Domain.Roles.Repositories;
using Domain.UserCommunicationChannels;
using Domain.UserCommunicationChannels.Repositories;
using Domain.Users.Repositories;
using Domain.Users.UserDetails;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Users.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserRegistrationResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEmailProvider _emailProvider;
    private readonly IRoleRepository _roleRepository;
    private readonly ICommunicationChannelRepository _communicationChannelRepository;
    private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
    private readonly IHashProvider _hashProvider;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger, IMapper mapper,
        IEmailProvider emailProvider, IRoleRepository roleRepository, ICommunicationChannelRepository communicationChannelRepository,
        IUserCommunicationChannelRepository userCommunicationChannelRepository, IHashProvider hashProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _emailProvider = emailProvider;
        _roleRepository = roleRepository;
        _communicationChannelRepository = communicationChannelRepository;
        _userCommunicationChannelRepository = userCommunicationChannelRepository;
        _hashProvider = hashProvider;
    }

    public async Task<Result<UserRegistrationResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User registration has been requested");
        var email = EmailAddress.BuildEmail(request.RegistrationDto.EmailAddress);
        if (!email.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid email", email.Error);
        }

        var firstName = Name.BuildName(request.RegistrationDto.FirstName);
        if (!firstName.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid firstname", firstName.Error);
        }

        var lastName = Name.BuildName(request.RegistrationDto.LastName);
        if (!lastName.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid lastname", lastName.Error);
        }

        var password = Password.BuildPassword(request.RegistrationDto.Password);
        if (!password.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid password", password.Error);
        }

        List<RoleName> roleNameCollection = new List<RoleName>();
        foreach (var role in request.RegistrationDto.Roles)
        {
            var roleName = RoleName.BuildRoleName(role);
            if (!roleName.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid role id", roleName.Error);
            }
            roleNameCollection.Add(roleName.Value!);
        }

        ICollection<Role> roles =
            await _roleRepository.GetRolesByNameCollectionAsync(roleNameCollection, cancellationToken);

        CommunicationChannel? communicationChannel = await _communicationChannelRepository
            .GetCommunicationChannelByIdAsync((int)CommunicationChannelType.Email, cancellationToken);
        
        password = Password.BuildHashed(_hashProvider.GetHash(password.Value!.Value));

        User newUser = new(
            Guid.NewGuid(),
            email.Value!,
            firstName.Value!,
            lastName.Value!,
            password.Value!,
            new List<UserCommunicationChannel>(),
            roles
        );
        var possibleUser = await _userRepository.GetUserByAsync(u => u.Email == email.Value, cancellationToken);
        if (possibleUser is not null)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("User with such email exists",
                new Error("Users.CreateUserCommandHandler", "User with such email exists", (int)HttpStatusCode.Conflict));
        }
        
        var result = await _userRepository.CreateAsync(newUser);
        
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (result != null)
        {
            Guid confirmationToken = Guid.NewGuid();

            await _userCommunicationChannelRepository.CreateAsync(new UserCommunicationChannel(Guid.NewGuid(), newUser,
                newUser.Id, false,
                confirmationToken,
                communicationChannel, (int)CommunicationChannelType.Email), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            
            EmailMessageComposer messageComposer = new EmailMessageComposer()
            {
                CopyTo = null,
                Recipient = email.Value!,
                ConfirmationEmail = new ConfirmationEmail()
                {
                    ConfirmationToken = confirmationToken,
                    UserId = newUser.Id
                }
            };

            await _emailProvider.SendAsync(messageComposer);
            
            _logger.LogInformation("User logged in successfully: Id = {UserId}", result.Id);
            
            UserRegistrationResponseDto userRegistration = _mapper.Map<UserRegistrationResponseDto>(result);
            
            return Result<UserRegistrationResponseDto>.Success(userRegistration);
        }

        return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("User registration failed, something wrong",
            new Error("Users.CreateUserCommandHandler", "Something wrong"));

    }
}