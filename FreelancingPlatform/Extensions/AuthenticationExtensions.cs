using FreelancingPlatform.OptionsConfiguration;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FreelancingPlatform.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidIssuer = configuration.GetSection("Jwt").Get<JwtOptions>()!.Issuer,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt").Get<JwtOptions>()!.SecretKey)),
                    ValidateAudience = false
                };
            });

            services.ConfigureOptions<JwtBearerOptionsSetup>();
            services.ConfigureOptions<JwtOptionsSetup>();
            return services;
        }
    }
}
