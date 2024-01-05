using Application.Models;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        JwtCredentials GenerateCredentials(Guid userId, string email);
    }
}
