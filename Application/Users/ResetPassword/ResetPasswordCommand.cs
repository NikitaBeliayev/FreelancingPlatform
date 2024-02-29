using Application.Abstraction.Messaging;
using Application.Users.ResponseDto;

namespace Application.Users.ResetPassword;
public record ResetPasswordCommand(Guid UserId, string Password, Guid Token) : ICommand<ResetPasswordResponseDto>
{
}
