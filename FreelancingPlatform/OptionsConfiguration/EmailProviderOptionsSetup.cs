using Infrastructure.EmailProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsConfiguration
{
    public class EmailProviderOptionsSetup : IConfigureOptions<EmailOptions>
    {
        private readonly IConfiguration _configuration;

        public EmailProviderOptionsSetup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void Configure(EmailOptions options)
        {
            const string section = "Email";
            _configuration.GetSection(section).Bind(options);
        }
    }
}
