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
    }
}
