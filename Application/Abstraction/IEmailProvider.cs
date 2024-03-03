using Application.Models;
using Application.Models.Email;
using Shared;

namespace Application.Abstraction
{
    public interface IEmailProvider
    {
        Task<Result> SendAsync(EmailMessageComposerModel emailModel, CancellationToken cancellationToken = default);
        int ResendMinutesDelay { get; }
        string ConfirmationEmailBody { get; }
        string ResetPasswordEmailBody { get; }
    }
}
