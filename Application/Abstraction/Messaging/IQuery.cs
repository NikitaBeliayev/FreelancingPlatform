using MediatR;
using Shared;

namespace Application.Abstraction.Messaging
{
    public interface IQuery<TResult>: IRequest<Result<TResult>>
    {
    }
}
