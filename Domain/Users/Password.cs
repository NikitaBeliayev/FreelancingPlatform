using Domain.Users.Errors;
using Shared;

namespace Domain.Users
{
    public class Password
    {
        private string? _value;
        public string Value
        {
            get => _value;
            set
            {
                if (_isValidated && _value is null || IsHashed(value))
                {
                    _value = value;
                    _isValidated = false;
                }
            }
        }

        private static bool _isValidated = false;

        private Password(string value) => Value = value;

        public static Result<Password> BuildPassword(string password)
        {
            var validationResult = ValidatePassword(password);

            if (validationResult.IsSuccess)
            {
                _isValidated = true;
                return Result<Password>.Success(new Password(password));
            }
            return Result<Password>.Failure(null, validationResult.Error);
        }
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Password> BuildPasswordWithoutValidation(string value)
        {
            _isValidated = true;
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

        private static bool IsHashed(string hashedPassword)
        {
            return hashedPassword.Length is 64 && hashedPassword.All(c => "0123456789abcdef".Contains(c));
        }
    }
}
