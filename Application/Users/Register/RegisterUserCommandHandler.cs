using System.Net;
using Application.Abstraction;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models.Email;
using Application.Users.Create;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.CommunicationChannels;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users;
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
        var email = Email.BuildEmail(request.RegistrationDto.Email);
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
        
        password.Value!.Value = _hashProvider.GetHash(password.Value!.Value);

        var roles = new List<Role>();
        foreach (var role in  new [] { request.RegistrationDto.Role })
        {
            var roleName = RoleName.BuildRoleName(role);
            if (!roleName.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("Invalid role id", roleName.Error);
            }
            roles.Add(new Role(role)
            {
                Name = RoleName.BuildRoleName(role).Value!
            });
        }

        _roleRepository.ChangeStateToUnchangedForCollection(roles);
        CommunicationChannel communicationChannel = new CommunicationChannel(CommunicationChannelNameVariations.Email)
        {
            Name = CommunicationChannelName.
                BuildCommunicationChannelName(CommunicationChannelNameVariations.GetValue(CommunicationChannelNameVariations.Email).Value!).Value! // change this to the more flexible solution
        };
        _communicationChannelRepository.ChangeStateToUnchanged(communicationChannel);
        
        User newUser = new(
            Guid.NewGuid(),
            email.Value!,
            firstName.Value!,
            lastName.Value!,
            password.Value!,
            new List<UserCommunicationChannel>(),
            roles,
            new List<Objective>(),
            new List<Objective>()
        );
        var possibleUser = await _userRepository.GetByExpressionWithIncludesAsync(u => u.Email == email.Value, cancellationToken);
        if (possibleUser is not null)
        {
            return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("User with such email exists",
                new Error("Users.CreateUserCommandHandler", "User with such email exists", (int)HttpStatusCode.Conflict));
        }
        
        var result = await _userRepository.CreateAsync(newUser, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (result != null)
        {
            Guid confirmationToken = Guid.NewGuid();

            await _userCommunicationChannelRepository.CreateAsync(new UserCommunicationChannel(Guid.NewGuid(), newUser,
                newUser.Id, false,
                confirmationToken,
                communicationChannel, Guid.Empty, DateTime.UtcNow), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            
            EmailMessageComposerModel messageComposerModel = new EmailMessageComposerModel()
            {
                CopyTo = null,
                Recipient = email.Value!,
                Content = new ConfirmationEmailModel()
                {
                    ConfirmationToken = confirmationToken,
                    EmailBody = _emailProvider.ConfirmationEmailBody,
                }
            };

            await _emailProvider.SendAsync(messageComposerModel, cancellationToken);
            
            _logger.LogInformation("User successfully registered with Id = {UserId}", result.Id);
            
            UserRegistrationResponseDto userRegistration = _mapper.Map<UserRegistrationResponseDto>(result);
            
            return Result<UserRegistrationResponseDto>.Success(userRegistration);
        }

        return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("User registration failed, something wrong",
            new Error("Users.CreateUserCommandHandler", "Something wrong"));
    }
}