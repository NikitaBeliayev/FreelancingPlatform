using Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;

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
            _configuration.GetSection("JwtSecretKey").Bind("SecretKey", options);
            _configuration.GetSection("Jwt").Bind("Issuer", options);
            _configuration.GetSection("Jwt").Bind("RefreshTokenLifeTimeInDays", options);
            _configuration.GetSection("Jwt").Bind("AccessTokenLifeTimeInMinutes", options);
        }
    }
}
