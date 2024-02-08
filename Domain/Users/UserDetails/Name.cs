using Domain.Users.Errors;
using Shared;

namespace Domain.Users.UserDetails
{
    public sealed record Name
    {
        public string Value { get; }
        private Name(string value) => Value = value;

        public static Result<Name> BuildName(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result<Name>.Failure(null, NameErrors.NullOrEmpty);

            return Result<Name>.Success(new Name(value));
        }
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Name> BuildNameWithoutValidation(string value)
        {
            return Result<Name>.Success(new Name(value));
        }
    }
}
