using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.ResetPasswordEmail;

public record SendResetPasswordEmailCommand(UserResetPasswordEmailRequestDto UserResetPasswordEmailRequestDto) : ICommand<ResetPasswordResponseDto>
{
}