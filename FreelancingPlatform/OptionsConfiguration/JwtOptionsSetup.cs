using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsConfiguration
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            _configuration.GetSection("Jwt").Bind(options);
        }
    }
}
