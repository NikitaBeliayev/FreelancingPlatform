using Shared;

namespace Domain.Users
{
    public class Password
    {
        public string Value { get; }

        private Password(string value) => Value = value;

        public static Result<Password> BuildPassword(string value)
        {
            var validationResult = ValidatePassword(value);

            return validationResult.IsSuccess
                ? Result<Password>.Success(new Password(value))
                : Result<Password>.Failure(null, validationResult.Error);
        }

        private static Result ValidatePassword(string value)
        {
            if (value.Length < 8 || value.Length > 14)
            {
                return Result.Failure(PasswordErrors.Length);
            }

            if (!value.Any(char.IsUpper) || !value.Any(char.IsLower))
            {
                return Result.Failure(PasswordErrors.Case);
            }

            if (!value.Any(char.IsDigit))
            {
                return Result.Failure(PasswordErrors.Digit);
            }

            if (value.Contains(' '))
            {
                return Result.Failure(PasswordErrors.Space);
            }

            if (!value.Any(c => !char.IsLetterOrDigit(c)))
            {
                return Result.Failure(PasswordErrors.SpecialChar);
            }

            return Result.Success();
        }
    }
}
