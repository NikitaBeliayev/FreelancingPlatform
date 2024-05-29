using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace FreelancingPlatform.Extensions
{
    public static class ControllersExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = Result.Failure(
                            new Error(typeof(ControllersExtensions).Namespace, "Invalid request model", 422));

                        return new UnprocessableEntityObjectResult(result)
                        {
                            ContentTypes = { "application/json" }
                        };
                    };
                });
            return services;
        }
    }
}