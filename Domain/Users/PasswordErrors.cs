using Shared;

namespace Domain.Users
{
    public static class PasswordErrors
    {
        public static Error Length =>
            new Error("Users.Password.Length", "Password must be between 8 and 14 characters long", 422);

        public static Error Case =>
            new Error("Users.Password.Case", "Password must contain both uppercase and lowercase characters", 422);

        public static Error Digit =>
            new Error("Users.Password.Digit", "Password must contain at least one numeric digit", 422);

        public static Error Space =>
            new Error("Users.Password.Space", "Password must not contain spaces", 422);

        public static Error SpecialChar =>
            new Error("Users.Password.SpecialChar", "Password must contain at least one special character", 422);
    }
}