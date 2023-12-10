using MediatR;
using Shared;

namespace Application.Abstraction.Messaging
{
    public interface ICommandHandler<TCommand>: IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }
    public interface ICommandHundler<TCommand, TResult>: IRequestHandler<TCommand, Result<TResult>>
        where TCommand : ICommand<TResult>
    {
    }
}
