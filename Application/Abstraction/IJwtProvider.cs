using Application.Models;
using Domain.Roles;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        JwtCredentials GenerateCredentials(Guid userId, string email, IEnumerable<string> roles);
    }
}
