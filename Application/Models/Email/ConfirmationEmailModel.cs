namespace Application.Models.Email;

public class ConfirmationEmailModel : EmailModel
{
    public override string ComposeBody(Guid token)
    {
        return base.EmailBody.Replace("UIUrl/confirm/{token:Guid}", $"UIUrl/confirm/{token}");
    }
}