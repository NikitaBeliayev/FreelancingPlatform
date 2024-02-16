using System.Net;
using Application.Abstraction;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Users.ResponseDto;
using Domain.Repositories;
using Domain.Users;
using Domain.Users.UserDetails;
using Shared;

namespace Application.Users.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, UserLoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashProvider _hashProvider;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHashProvider hashProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashProvider = hashProvider;
    }
    public async Task<Result<UserLoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<EmailAddress> emailAddress = EmailAddress.BuildEmail(request.LoginUserDto.EmailAddress.ToLower());
        if (!emailAddress.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid email format", emailAddress.Error);
        }

        User? possibleUser =
            await _userRepository.GetByExpressionWithIncludesAsync(
                user => user.Email == emailAddress.Value, cancellationToken, user => user.Roles);

        if (possibleUser is null)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("There is no user with this email address", new Error("User.LoginUserCommandHandler"
                , "There is no user with this email address", (int)HttpStatusCode.Unauthorized));
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

        JwtCredentials jwtCredentials = _jwtProvider.GenerateCredentials(possibleUser.Id, possibleUser.Email.Value, possibleUser.Roles.Select(r => r.Name.Value));

        return Result<UserLoginResponseDto>.Success(new UserLoginResponseDto()
        {
            Id = possibleUser.Id,
            Credentials =  jwtCredentials
        });

    }
}