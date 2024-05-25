using FreelancingPlatform.OptionsConfiguration;
using FreelancingPlatform.OptionsValidation;
using Infrastructure.Authentication;
using Infrastructure.DatabaseConfiguration;
using Infrastructure.EmailProvider;
using Infrastructure.HashProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.Extensions
{
    public static class OptionsValidationExtensions
    {
        public static IServiceCollection AddOptionsValidation(this IServiceCollection services)
        {
            services.ConfigureOptions<EmailProviderOptionsSetup>();
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services.ConfigureOptions<HashOptionsSetup>();

            services.AddSingleton<IValidateOptions<EmailOptions>, EmailOptionsValidation>();
            services.AddSingleton<IValidateOptions<JwtOptions>, JwtOptionsValidation>();
            services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidation>();
            services.AddSingleton<IValidateOptions<HashOptions>, HashOptionsValidation>();

            return services;
        }
    }
}