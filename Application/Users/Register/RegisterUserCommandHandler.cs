using System.Net;
using Application.Abstraction;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Users.Create;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.UserCommunicationChannels;
using Domain.Users;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserRegistrationResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    
    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger, IMapper mapper, 
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
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
        
        User newUser = new(
            Guid.NewGuid(),
            email.Value!,
            firstName.Value!,
            lastName.Value!,
            password.Value!,
            new List<UserCommunicationChannel>()
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
            _logger.LogInformation("User logged in successfully: Id = {UserId}", result.Id);

            UserRegistrationResponseDto userRegistration = _mapper.Map<UserRegistrationResponseDto>(result);
            
            return Result<UserRegistrationResponseDto>.Success(userRegistration);
        }

        return ResponseHelper.LogAndReturnError<UserRegistrationResponseDto>("User registration failed, something wrong",
            new Error("Users.CreateUserCommandHandler", "Something wrong"));

    }
}