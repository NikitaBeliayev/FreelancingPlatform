namespace Application.Models.Email;

public class ResetPasswordEmailModel : EmailModel
{
    public override string ComposeBody(Guid token)
    {
        return base.EmailBody.Replace("https://students-hub.tihomirov.dev/auth/reset-password/update?token={token}",
            $"https://students-hub.tihomirov.dev/auth/reset-password/update?token={token}");
    }
}