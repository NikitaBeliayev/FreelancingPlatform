namespace Application.Users.ResponseDto;

public class ResetPasswordResponseDto
{
	public Guid Id { get; set; }
	public bool Success { get; set; }
	public string Message { get; set; }
}