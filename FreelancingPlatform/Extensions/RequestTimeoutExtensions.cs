using Microsoft.AspNetCore.Http.Timeouts;

namespace FreelancingPlatform.Extensions
{
    public static class RequestTimeoutExtensions
    {
        public static IServiceCollection AddCustomRequestTimeouts(this IServiceCollection services, IConfiguration configuration)
        {
            var requestTimeout = configuration.GetSection("RequestTimeout").GetValue<int>("DefaultInSeconds");

            services.AddRequestTimeouts(options =>
            {
                options.DefaultPolicy = new RequestTimeoutPolicy()
                {
                    Timeout = TimeSpan.FromSeconds(requestTimeout),
                    TimeoutStatusCode = StatusCodes.Status408RequestTimeout
                };
            });
            return services;
        }
    }
}
