using Application.Models.Jwt;
using System.Security.Claims;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        JwtCredentials GenerateCredentials(Guid userId, string email, IEnumerable<string> roles);
        ClaimsPrincipal ValidateToken(string token);
    }
}
