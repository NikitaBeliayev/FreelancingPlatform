using Shared;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) => new("Users.NotFound", $"The user with id {id} has not been found", 404);
		public static Error InvalidConfirmationToken() => new("Users.InvalidConfirmationToken", $"The confirmation token is invalid", 400);
		public static Error InvalidConfirmationToken(Guid id) => new("Users.InvalidConfirmationToken", $"The confirmation token for user with id {id} is invalid", 400);
        public static Error EmailChannelMissing() => new("Users.EmailChannelMissing", "Email communication channel not found", 400);
        public static Error EmailChannelMissing(Guid id) => new("Users.EmailChannelMissing", $"Email communication channel for user with id {id} is missing", 400);
        public static Error EmailAlreadyVerified(Guid id) => new("Users.EmailAlreadyVerified", $"Email is already verified for user with id {id}", 400);
        public static Error EmailNotVerified(Guid id) => new("Users.EmailNotVerified", $"Email is not verified for user with id {id}", 403);
        public static Error ResendEmailDelayNotMet(Guid id) => new("Users.ResendEmailDelayNotMet", $"Cannot resend email to user with id {id} until the required delay has passed", 400);
        public static Error TooManyRequests(Guid id) => new("Users.TooManyRequests", $"Too many requests for user with id {id}", 429);
        public static Error InvalidEmailFormat(string email) => new("Users.InvalidEmailFormat", $"The email {email} is invalid", 400);
        public static Error UserNotFound(string email) => new("Users.UserNotFound", $"The user with email {email} does not exist", 404);
    }
}