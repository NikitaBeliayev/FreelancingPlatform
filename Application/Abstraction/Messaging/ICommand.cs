using MediatR;
using Shared;

namespace Application.Abstraction.Messaging
{
    public interface ICommand: IRequest<Result>
    {
    }

    public interface ICommand<TResult>: IRequest<Result<TResult>>
    {
    }
}
