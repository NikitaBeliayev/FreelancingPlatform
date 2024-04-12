using Application.Abstraction.Messaging;
using Application.Users.ResponseDto;

namespace Application.Users.ResetPassword;
public record ResetPasswordCommand(string Password, Guid Token) : ICommand<ResetPasswordResponseDto>
{
}
