using Serilog;

namespace FreelancingPlatform.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder AddCustomLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            return hostBuilder;
        }

        public static WebApplication UseCustomRequestLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
            return app;
        }
    }
}
