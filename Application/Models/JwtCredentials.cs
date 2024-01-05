namespace Application.Models;

public class JwtCredentials
{
    public string RefreshToken { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresIn { get; set; }
    public DateTime RefreshTokenExpiresIn { get; set; }
}