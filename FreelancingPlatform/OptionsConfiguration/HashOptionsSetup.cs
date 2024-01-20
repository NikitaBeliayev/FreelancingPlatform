using Infrastructure.HashProvider;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsConfiguration
{
    public class HashOptionsSetup : IConfigureOptions<HashOptions>
    {
        private readonly IConfiguration _configuration;

        public HashOptionsSetup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void Configure(HashOptions options)
        {
            _configuration.GetSection("Hashing").Bind(options);
        }
    }
}
