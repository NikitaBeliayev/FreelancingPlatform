using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsValidation
{
    public class JwtOptionsValidation : IValidateOptions<JwtOptions>
    {
        private readonly ILogger<JwtOptionsValidation> _logger;
        public JwtOptionsValidation(ILogger<JwtOptionsValidation> logger)
        {
            _logger = logger;
        }
        public ValidateOptionsResult Validate(string? name, JwtOptions options)
        {
            if (string.IsNullOrEmpty(options.Issuer))
            {
                _logger.LogError("Issuer can't be empty.");
                throw new Exception("Issuer can't be empty.");
            }
            else if (options.AccessTokenLifeTimeInMinutes is not 20)
            {
                _logger.LogError("Invalid value of lifetime for access token.");
                throw new Exception("Invalid value of lifetime for access token.");
            }
            else if (options.RefreshTokenLifeTimeInDays is not 10)
            {
                _logger.LogError("Invalid value of lifetime for refresh token.");
                throw new Exception("Invalid value of lifetime for refresh token.");
            }
            else if (string.IsNullOrEmpty(options.SecretKey))
            {
                _logger.LogError("Secret key can't be empty.");
                throw new Exception("Secret key can't be empty.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
