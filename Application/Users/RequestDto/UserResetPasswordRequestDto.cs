namespace Application.Users.RequestDto;

public class UserResetPasswordRequestDto
{
    public string Password { get; set; }
    public Guid Token { get; set; }
}
