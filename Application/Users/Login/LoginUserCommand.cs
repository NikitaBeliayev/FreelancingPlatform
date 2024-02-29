using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.Login;

public record LoginUserCommand(UserLoginDto LoginUserDto) : ICommand<UserLoginResponseDto>
{
}