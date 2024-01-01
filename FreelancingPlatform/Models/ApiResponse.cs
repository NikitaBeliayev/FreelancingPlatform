
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace FreelancingPlatform.Models
{
	public class ApiResponse<T> : ApiResponse
	{
		public T? Data { get; }

        protected ApiResponse(T? data, bool isSuccess, string message, List<string>? errors, int responseCode)
                :base(isSuccess, message, errors, responseCode)
        {
            this.Data = data;
        }

        public static ApiResponse<T> Success(T? data, string message)
            => new (data, true, message, null, 200);
        public static ApiResponse<T> Failure(T? data, string message, List<string>? errors, int responseCode = 500)
            => new (data, false, message, errors, responseCode);

        public static ApiResponse<T> FromResult(Result<T> result, int statusCode = 200) => new(result.Value,
            result.IsSuccess,
            result.Error == Error.None ? "OK" : result.Error.Message,
            result.Error == Error.None ? [] : [$"{result.Error.Code} (${result.Error.StatusCode}): {result.Error.Message}"],
            result.IsSuccess ? statusCode : result.Error.StatusCode);

        public static implicit operator ObjectResult(ApiResponse<T> response) =>
            new(response) { StatusCode = response.StatusCode };
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public List<string>? Errors { get; }
        public int StatusCode { get; }

        protected ApiResponse(bool success, string message, List<string>? errors, int responseCode)
        {
            this.IsSuccess = success;
            this.Message = message;
            this.Errors = errors;
            this.StatusCode = responseCode;
        }

        public static ApiResponse Success(string message)
            => new(true, message, null, 200);
        public static ApiResponse Failure(string message, List<string>? errors, int responseCode = 500)
            => new(false, message, errors, responseCode);
    }
}