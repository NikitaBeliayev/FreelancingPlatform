namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid id, string email);
        string GenerateRefreshToken(Guid id, string email);
    }
}
