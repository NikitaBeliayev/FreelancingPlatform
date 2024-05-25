using Infrastructure.Automapper;

namespace FreelancingPlatform.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            return services;
        }
    }
}
