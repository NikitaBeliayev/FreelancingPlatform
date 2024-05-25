using Scrutor;

namespace FreelancingPlatform.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.Scan(
                x => x.FromAssemblies(
                        Infrastructure.MetaData.AssemblyInfo.Assembly
                    )
                .AddClasses(false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddMediatR(
                x => x.RegisterServicesFromAssembly(Application.MetaData.AssemblyInfo.Assembly));

            return services;
        }
    }
}
