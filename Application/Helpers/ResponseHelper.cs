using Shared;
using System;

namespace Application.Helpers
{
    public static class ResponseHelper
    {
        public static Result<T> LogAndReturnError<T>(string message, Error error)
        {
            Console.WriteLine($"Error: {message}, {error.Code}: {error.Message}");
            return Result<T>.Failure(default, error);
        }
    }
}