namespace Application.Models.Email;

public abstract class EmailModel
{
    public Guid ConfirmationToken { get; set; }
    public string EmailBody { get; set; } = null!;
    public abstract string ComposeBody(Guid token);
}