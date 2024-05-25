namespace FreelancingPlatform.Extensions
{
    public static class ControllersExtensions
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
    }
}
