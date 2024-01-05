using System.Net;
using Application.Abstraction;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Users.ResponseDto;
using Domain.Users;
using Shared;

namespace Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, UserLoginResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<Result<UserLoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        Result<EmailAddress> emailAddress = EmailAddress.BuildEmail(request.LoginUserDto.EmailAddress.ToLower());
        if (!emailAddress.IsSuccess)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid email format", emailAddress.Error);
        }

        User? possibleUser =
            await _userRepository.GetUserByAsync(
                u => u.Email == emailAddress.Value, cancellationToken);

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

        if (possibleUser.Password.Value != password.Value!.Value)
        {
            return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid password", new Error("User.LoginUserCommandHandler",
                "Invalid password", (int)HttpStatusCode.Unauthorized));
        }

        JwtCredentials jwtCredentials = _jwtProvider.GenerateCredentials(possibleUser.Id, possibleUser.Email.Value);

        return Result<UserLoginResponseDto>.Success(new UserLoginResponseDto()
        {
            Id = possibleUser.Id,
            Credentials =  jwtCredentials
        });

    }
}