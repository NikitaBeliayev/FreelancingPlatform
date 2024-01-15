using Application.Abstraction.Messaging;

namespace Application.Users.Create
{
    public record CreateUserCommand(UserDto User): ICommand<UserDto>
    {
    }
}
