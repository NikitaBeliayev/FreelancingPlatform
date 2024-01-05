using System.Windows.Input;
using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.LoginUser;

public record LoginUserCommand(UserLoginDto LoginUserDto) : ICommand<UserLoginResponseDto>
{
    
}