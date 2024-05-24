using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstraction;
using Application.Models.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        private readonly ILogger<JwtProvider> _logger;

        public JwtProvider(IOptions<JwtOptions> options, ILogger<JwtProvider> logger)
        {
            this._options = options.Value;
            this._logger = logger;
        }

        public JwtCredentials GenerateCredentials(Guid userId, string email, IEnumerable<string> roles, IEnumerable<string> roleIds)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
			};

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            claims.AddRange(roleIds.Select(roleId => new Claim("roleId", roleId)));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                _options.Issuer,
                default,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenLifeTimeInMinutes),
                signingCredentials
            );

            var refreshToken = new JwtSecurityToken(
                _options.Issuer,
                default,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_options.RefreshTokenLifeTimeInDays),
                signingCredentials
            );

            var jwtHandler = new JwtSecurityTokenHandler();

            JwtCredentials credentials = new JwtCredentials
            {
                AccessToken = jwtHandler.WriteToken(accessToken),
                RefreshToken = jwtHandler.WriteToken(refreshToken),
                AccessTokenExpiresIn = DateTime.Now.AddMinutes(_options.AccessTokenLifeTimeInMinutes),
                RefreshTokenExpiresIn = DateTime.Now.AddDays(_options.RefreshTokenLifeTimeInDays)
            };

            _logger.LogInformation("Token: {Token}", credentials.AccessToken);

            return credentials;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                _logger.LogError("Cannot read token");
                throw new ArgumentException("Invalid token format");
            }

            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken == null)
            {
                _logger.LogError("Failed to decode token");
                throw new ArgumentException("Invalid token format");
            }

            _logger.LogInformation("Token claims: {Claims}", jwtToken.Claims.Select(c => $"{c.Type}: {c.Value}"));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken validatedToken;
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating token");
                throw;
            }

            return principal;
        }
    }
}
