using Domain.Users.UserDetails;

namespace Application.Models
{
    public class EmailMessageComposer
    {
        public EmailAddress? CopyTo { get; set; }
        public string? Body { get; set; }
        public EmailAddress Recipient { get; set; } = null!;
        public ConfirmationEmail? ConfirmationEmail { get; set; }
    }
}
