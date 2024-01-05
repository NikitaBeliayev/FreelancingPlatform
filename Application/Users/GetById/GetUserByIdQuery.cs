using Application.Abstraction.Messaging;

namespace Application.Users.GetById
{
    public record GetUserByIdQuery(Guid UserId): IQuery<UserDto>
    {
    }
}
