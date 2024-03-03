using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.EmailConfirm;

public record ConfirmUserEmailCommand(UserConfirmEmailRequestDto UserConfirmEmailRequestDto) : ICommand<UserEmailConfirmationResponseDto>
{
}