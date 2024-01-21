using Infrastructure.EmailProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsValidation
{
    public class EmailOptionsValidation : IValidateOptions<EmailOptions>
    {
        private readonly ILogger<EmailOptionsValidation> _logger;

        public EmailOptionsValidation(ILogger<EmailOptionsValidation> logger)
        {
            _logger = logger;
        }
        public ValidateOptionsResult Validate(string? name, EmailOptions options)
        {
            if (options.Port is not 587 &&
                options.Port is not 25 && 
                options.Port is not 465)
            {
                _logger.LogError("Incorrect ports are set for the mail client.");
                throw new Exception("Incorrect ports are set for the mail client.");
            }
            else if (string.IsNullOrEmpty(options.Host))
            {
                _logger.LogError("Email client's host can't be empty.");
                throw new Exception("Email client's host can't be empty.");
            }
            else if (string.IsNullOrEmpty(options.SenderEmail))
            {
                _logger.LogError("Email of smtp client can't be empty.");
                throw new Exception("Email of smtp client can't be empty.");
            }
            else if (string.IsNullOrEmpty(options.Password))
            {
                _logger.LogError("Email client's password can't be empty.");
                throw new Exception("Email client's password can't be empty.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
