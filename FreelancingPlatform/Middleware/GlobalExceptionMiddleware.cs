using Application.Models;

namespace FreelancingPlatform.Middleware
{
	public class GlobalExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger;

		public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unhandled exception occurred.");

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;

				var response = new ApiResponse<object>
				{
					Success = false,
					Message = "An internal server error occurred.",
					Errors = ["Internal Server Error"]
				};

				await context.Response.WriteAsJsonAsync(response);
			}
		}
	}

	public static class GlobalExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<GlobalExceptionMiddleware>();
		}
	}
}
