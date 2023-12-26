using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FreelancingPlatform.Middleware;

namespace Tests.GlobalExceptionMiddlewareTests
{
	[TestFixture]
	public class GlobalExceptionMiddlewareTests
	{
		[Test]
		public async Task InvokeAsync_Exception_LogsErrorAndReturnsInternalServerError()
		{
			// Arrange
			var logger = new FakeLogger<GlobalExceptionMiddleware>();
			var middleware = new GlobalExceptionMiddleware(NextMiddleware, logger);

			var context = new DefaultHttpContext();
			context.Response.Body = new MemoryStream();

			Task NextMiddleware(HttpContext ctx)
			{
				throw new Exception("Test exception");
			}

			// Act
			await middleware.InvokeAsync(context);

			// Assert
			Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
			logger.AssertLogged("An unhandled exception occurred.");
		}

		[Test]
		public async Task InvokeAsync_NoException_PassesToNextMiddleware()
		{
			// Arrange
			var logger = new FakeLogger<GlobalExceptionMiddleware>();
			var middleware = new GlobalExceptionMiddleware(NextMiddleware, logger);

			var context = new DefaultHttpContext();
			context.Response.Body = new MemoryStream();

			async Task NextMiddleware(HttpContext ctx)
			{
				await Task.FromResult(true);
			}

			// Act
			await middleware.InvokeAsync(context);

			// Assert
			Assert.That(context.Response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
			logger.AssertNotLogged(LogLevel.Error);
		}

		public class FakeLogger<T> : ILogger<T> 
		{
			private readonly List<string> _logMessages = new List<string>();

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
            {
				if (exception is not null)
                    _logMessages.Add(formatter(state, exception));
            }

			public bool IsEnabled(LogLevel logLevel) => true; // Assuming logging is always enabled for the test

			public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

			public void AssertLogged(string expectedMessage)
			{
				var loggedMessage = _logMessages.SingleOrDefault(msg => msg.Contains(expectedMessage));

				Assert.That(loggedMessage, Is.Not.Null, $"Expected log message containing: '{expectedMessage}'");
			}

			public void AssertNotLogged(LogLevel logLevel)
			{
				var notLogged = _logMessages.All(msg => !msg.Contains(logLevel.ToString()));

				Assert.That(notLogged, Is.True, $"Expected no log messages with level: '{logLevel}'");
			}
		}
	}
}
