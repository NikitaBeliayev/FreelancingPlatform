using Infrastructure.Authentication;
using Infrastructure.HashProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsValidation
{
    public class HashOptionsValidation : IValidateOptions<HashOptions>
    {
        private readonly ILogger<HashOptionsValidation> _logger;
        public HashOptionsValidation(ILogger<HashOptionsValidation> logger)
        {
            _logger = logger;
        }
        public ValidateOptionsResult Validate(string? name, HashOptions options)
        {
            if (string.IsNullOrEmpty(options.secretPepper))
            {
                _logger.LogError("Pepper can't be empty.");
                throw new Exception("Pepper can't be empty.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
