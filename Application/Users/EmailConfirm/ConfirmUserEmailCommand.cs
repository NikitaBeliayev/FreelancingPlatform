using Application.Abstraction.Messaging;
using Application.Users.ResponseDto;

namespace Application.Users.EmailConfirm;

public record ConfirmUserEmailCommand(Guid userId, Guid token) : ICommand<UserEmailConfirmationResponseDto>
{
}