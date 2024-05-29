namespace Application.Models.Email;

public class ConfirmationEmailModel : EmailModel
{
    public override string ComposeBody(Guid token)
    {
        return base.EmailBody.Replace("https://students-hub.tihomirov.dev/auth/confirm-email?token={token}",
            $"https://students-hub.tihomirov.dev/auth/confirm-email?token={token}");
    }
}