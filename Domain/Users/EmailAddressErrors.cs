using Shared;

namespace Domain.Users
{
    public static class EmailAddressErrors
    {
        public static Error InvalidFormat =>
            new("Users.EmailAddress.InvalidFormat", $"The email address has an invalid format.");
    }
}
