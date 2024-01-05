using Application.Abstraction.Messaging;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;

namespace Application.Users.RegisterUser;

public record RegisterUserCommand(UserRegistrationDto RegistrationDto) : ICommand<UserRegistrationResponseDto>
{
    
}