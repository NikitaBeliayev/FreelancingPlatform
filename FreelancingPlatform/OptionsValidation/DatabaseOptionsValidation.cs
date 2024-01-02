using Infrastructure.DatabaseConfiguration;
using Infrastructure.EmailProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsValidation
{
    public class DatabaseOptionsValidation : IValidateOptions<DatabaseOptions>
    {
        private readonly ILogger<DatabaseOptions> _logger;

        public DatabaseOptionsValidation(ILogger<DatabaseOptions> logger)
        {
            _logger = logger;
        }
        public ValidateOptionsResult Validate(string? name, DatabaseOptions options)
        {
            return ValidateOptionsResult.Success;
        }
    }
}