using System.Net;
using Application.Abstraction;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models.Jwt;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Domain.Users;
using Domain.Users.UserDetails;
using Shared;
using Domain.CommunicationChannels;
using Microsoft.Extensions.Logging;

namespace Application.Users.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, UserLoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashProvider _hashProvider;
    private readonly IMapper _mappper;
    private readonly ILogger<LoginUserCommandHandler> _logger;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHashProvider hashProvider, IMapper mappper, ILogger<LoginUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashProvider = hashProvider;
        _mappper = mappper;
        _logger = logger;
    }
    public async Task<Result<UserLoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User login requested");

        Result<Email> emailAddress = Email.BuildEmail(request.LoginUserDto.Email.ToLower());
        if (!emailAddress.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid email format", emailAddress.Error);
        }

        User? possibleUser =
            await _userRepository.GetByExpressionWithIncludesAsync(
                user => user.Email == emailAddress.Value, cancellationToken, user => user.Roles, user => user.CommunicationChannels);

        if (possibleUser is null)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("There is no user with this email address", new Error("User.LoginUserCommandHandler", 
                "There is no user with this email address", (int)HttpStatusCode.Unauthorized));
        }
        var emailConfirmed = possibleUser.CommunicationChannels.Any(ucc => ucc.CommunicationChannelId == CommunicationChannelNameVariations.Email && ucc.IsConfirmed);
        if (!emailConfirmed)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Email has not been confirmed", new Error("User.LoginUserCommandHandler", 
                "Email has not been confirmed", (int)HttpStatusCode.Unauthorized));
        }

        Result<Password> password = Password.BuildPassword(request.LoginUserDto.Password);
        if (!password.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid password format", password.Error);
        }
        password.Value!.Value = _hashProvider.GetHash(password.Value.Value);

        if (possibleUser.Password.Value != password.Value.Value)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid password", new Error("User.LoginUserCommandHandler",
                "Invalid password", (int)HttpStatusCode.Unauthorized));
        }

        JwtCredentials jwtCredentials = _jwtProvider.GenerateCredentials(possibleUser.Id, possibleUser.Email.Value, 
            possibleUser.Roles.Select(r => r.Name.Value), possibleUser.Roles.Select(r => r.Id.ToString()));

        _logger.LogInformation("User with Id = {id} successfully logged in", possibleUser.Id);
        return Result<UserLoginResponseDto>.Success(_mappper.Map<UserLoginResponseDto>(
            new Tuple<User, JwtCredentials>(possibleUser, jwtCredentials)));
    }
}