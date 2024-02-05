using Shared;

namespace Application.Helpers
{
    public static class ResponseHelper
    {
        public static Result<T> LogAndReturnError<T>(string message, Error error, T value = default)
        {
            Console.WriteLine($"Error: {message}, {error.Code}: {error.Message}");
            return Result<T>.Failure(value, error);
        }
    }
}