namespace Application.Models;

public class ConfirmationEmail
{
    public Guid UserId { get; set; }
    public Guid ConfirmationToken { get; set; }
}