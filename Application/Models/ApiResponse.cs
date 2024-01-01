
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Application.Models
{
	public class ApiResponse<T> : ApiResponse
	{
		public T? Data { get; }

        protected ApiResponse(T? data, bool isSuccess, string message, List<string>? errors)
                :base(isSuccess, message, errors)
        {
            this.Data = data;
        }

        public static ApiResponse<T> Success(T? data, string message) 
            => new (data, true, message, null);
        public static ApiResponse<T> Failure(T? data, string message, List<string>? errors)
            => new (data, false, message, errors);
    }

    public class ApiResponse
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public List<string>? Errors { get; }
        
        protected ApiResponse(bool success, string message, List<string>? errors)
        {
            this.IsSuccess = success;
            this.Message = message;
            this.Errors = errors;
        }

        public static ApiResponse Success(string message)
            => new(true, message, null);
        public static ApiResponse Failure(string message, List<string>? errors)
            => new(false, message, errors);
    }
}
