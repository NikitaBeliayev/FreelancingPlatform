namespace Application.Models.Email;

public class ResetPasswordEmailModel : EmailModel
{
    public override string ComposeBody(Guid token)
    {
        return base.EmailBody.Replace("UIUrl/reset/{token:Guid}", $"UIUrl/reset/{token}");
    }
}