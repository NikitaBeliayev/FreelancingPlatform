using Application.Models;

namespace Application.Users.ResponseDto;

public class UserLoginResponseDto
{
    public Guid Id { get; set; }
    public JwtCredentials Credentials { get; set; } = null!;
}