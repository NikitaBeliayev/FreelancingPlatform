using Infrastructure.DatabaseConfiguration;
using Microsoft.Extensions.Options;

namespace FreelancingPlatform.OptionsConfiguration;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public void Configure(DatabaseOptions options)
    {
        _configuration.GetSection("Database").Bind(options);
    }
}