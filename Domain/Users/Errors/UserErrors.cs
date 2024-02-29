using Shared;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) => new("Users.NotFound", $"The user with id {id} has not been found", 404);
        public static Error InvalidConfirmationToken(Guid id) => new("Users.InvalidConfirmationToken", $"The confirmation token for user with id {id} is invalid", 400);
        public static Error EmailChannelMissing(Guid id) => new("Users.EmailChannelMissing", $"Email communication channel for user with id {id} is missing", 400);
        public static Error EmailAlreadyVerified(Guid id) => new("Users.EmailAlreadyVerified", $"Email is already verified for user with id {id}", 400);
        public static Error ResendEmailDelayNotMet(Guid id) => new("Users.ResendEmailDelayNotMet", $"Cannot resend email to user with id {id} until the required delay has passed", 400);
    }
}