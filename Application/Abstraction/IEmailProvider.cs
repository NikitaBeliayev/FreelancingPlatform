using Application.Models;
using Shared;

namespace Application.Abstraction
{
    public interface IEmailProvider
    {
        Task<Result> SendAsync(EmailMessageComposer emailModel, CancellationToken cancellationToken = default);
    }
}
