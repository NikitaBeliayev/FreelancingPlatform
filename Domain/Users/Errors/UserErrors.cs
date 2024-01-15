using Shared;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) =>
            new("Users.NotFound", $"The user with id {id} has not been found", 404);
    }
}