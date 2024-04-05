using Domain.Users.Errors;
using Shared;

namespace Domain.Users.UserDetails
{
    public class Email
    {
        public string Value { get; }
        public bool IsEmailValidated { get; }

        private Email(string value, bool isEmailValidated)
        {
            Value = value;
            IsEmailValidated = isEmailValidated;
        }

        public static Result<Email> BuildEmail(string value)
        {
            var validationResult = ValidateEmail(value);

            return validationResult.IsSuccess
                ? Result<Email>.Success(new Email(value.ToLower(), isEmailValidated: true))
                : Result<Email>.Failure(null, validationResult.Error);
        }

        private static Result ValidateEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure(EmailAddressErrors.InvalidFormat);
            }

            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
            {
                return Result.Failure(EmailAddressErrors.InvalidFormat);
            }

            return Result.Success();
        }
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Email> BuildEmailWithoutValidation(string value)
        {
            return Result<Email>.Success(new Email(value.ToLower(), isEmailValidated: false));
        }
    }
}
