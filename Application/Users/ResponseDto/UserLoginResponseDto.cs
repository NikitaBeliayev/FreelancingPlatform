using Application.Models.Jwt;

namespace Application.Users.ResponseDto;

public class UserLoginResponseDto
{
    public Guid Id { get; set; }
    public JwtCredentials Credentials { get; set; } = null!;
}