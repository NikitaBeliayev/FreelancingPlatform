using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Abstraction;
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
        public string GenerateToken(Guid id, string email)
        {

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,  email), 
            };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _options.Issuer,
                default,
                claims,
                null,
                DateTime.UtcNow.AddMinutes(_options.AccessTokenLifeTimeInMinutes),
                signingCredentials
                );

            var jwtHandler = new JwtSecurityTokenHandler();

            return jwtHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(Guid id, string email)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,  email),
            };
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _options.Issuer,
                default,
                claims,
                null,
                DateTime.UtcNow.AddDays(_options.RefreshTokenLifeTimeInDays),
                signingCredentials
            );

            var jwtHandler = new JwtSecurityTokenHandler();

            return jwtHandler.WriteToken(token);
        }

    }
}
