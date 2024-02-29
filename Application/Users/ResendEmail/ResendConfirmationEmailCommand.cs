using Application.Users.ResponseDto;
using Application.Abstraction.Messaging;

namespace Application.Users.ResendEmail
{
    public record ResendConfirmationEmailCommand(Guid userId) : ICommand<UserResendEmailConfirmationResponseDto>
    {
    }
}
