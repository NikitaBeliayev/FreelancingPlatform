using Infrastructure.Automapper;
using Infrastructure.Automapper.Profiles;

namespace FreelancingPlatform.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ObjectiveProfile), typeof(ObjectiveTypeProfile), typeof(PaymentProfile), typeof(RoleProfile), typeof(UserProfile));
            return services;
        }
    }
}
