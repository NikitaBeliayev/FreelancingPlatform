using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.Register;

public record RegisterUserCommand(UserRegistrationDto RegistrationDto) : ICommand<UserRegistrationResponseDto>
{
    
}