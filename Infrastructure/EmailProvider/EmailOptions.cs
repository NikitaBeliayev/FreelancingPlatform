using Application.Abstraction;

namespace Infrastructure.EmailProvider
{
    public class EmailOptions
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = default;
        public string ConfirmationEmailBody { get; set; } = string.Empty;
        public string ResetPasswordEmailBody { get; set; } = string.Empty;
        public int ResendMinutesDelay { get; set; } = default;
    }
}
