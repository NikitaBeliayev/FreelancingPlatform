using Application.Models.Jwt;
using System.Security.Claims;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        JwtCredentials GenerateCredentials(Guid userId, string email, IEnumerable<string> roles, IEnumerable<string> roleIds);
        ClaimsPrincipal ValidateToken(string token);
    }
}
