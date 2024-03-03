using Domain.Users.UserDetails;

namespace Application.Models.Email
{
    public class EmailMessageComposerModel
    {
        public EmailAddress? CopyTo { get; set; }
        public EmailAddress Recipient { get; set; } = null!;
        public EmailModel Content { get; set; } = null!;
    }
}
