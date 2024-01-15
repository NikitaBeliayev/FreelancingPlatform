namespace Infrastructure.EmailProvider
{
    public class EmailOptions
    {
        public string SenderEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = default;
    }
}
