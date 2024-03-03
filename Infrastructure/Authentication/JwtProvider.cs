using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstraction;
using Application.Models.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            this._options = options.Value;
        }

        public JwtCredentials GenerateCredentials(Guid userId, string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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

            return new JwtCredentials
            { 
                AccessToken = jwtHandler.WriteToken(accessToken),
                RefreshToken = jwtHandler.WriteToken(refreshToken),
                AccessTokenExpiresIn = DateTime.Now.AddMinutes(_options.AccessTokenLifeTimeInMinutes),
                RefreshTokenExpiresIn = DateTime.Now.AddDays(_options.RefreshTokenLifeTimeInDays)
            };
        }
    }
}
