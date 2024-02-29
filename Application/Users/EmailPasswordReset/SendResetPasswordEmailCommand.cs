using Application.Abstraction.Messaging;
using Application.Users.ResponseDto;

namespace Application.Users.EmailPasswordReset;

public record SendResetPasswordEmailCommand(string userEmail) : ICommand<ResetPasswordResponseDto>
{
}