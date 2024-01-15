using MediatR;
using Shared;

namespace Application.Abstraction.Messaging
{
    public interface IQueryHandler<TQuery, TResult>: IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
