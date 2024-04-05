using Domain.Users.UserDetails;

namespace Application.Models.Email
{
    public class EmailMessageComposerModel
    {
        public Domain.Users.UserDetails.Email? CopyTo { get; set; }
        public Domain.Users.UserDetails.Email Recipient { get; set; } = null!;
        public EmailModel Content { get; set; } = null!;
    }
}
