using Domain.Users.Errors;
using Shared;

namespace Domain.Users.UserDetails
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
        public static Result<Password> BuildHashed(string value)
        {
            return Result<Password>.Success(new Password(value));
        }
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Password> BuildHashedWithoutValidation(string value)
        {
            return Result<Password>.Success(new Password(value));
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
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Password> BuildPasswordWithoutValidation(string value)
        {
            return Result<Password>.Success(new Password(value));
        }
    }
}
