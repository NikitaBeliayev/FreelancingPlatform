namespace Application.Users.RequestDto;

public class UserLoginDto
{
    public string EmailAddress { get; set; } = null!;
    public string Password { get; set; } = null!;
}   