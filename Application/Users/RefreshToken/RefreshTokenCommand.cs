using Application.Abstraction.Messaging;
using Application.Users.ResponseDto;

namespace Application.Users.RefreshToken
{
    public record RefreshTokenCommand(string Token) : ICommand<UserLoginResponseDto>
    {

    }
}
