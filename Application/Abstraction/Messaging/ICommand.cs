using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Messaging
{
    public interface ICommand: IRequest<Result>
    {
    }

    public interface ICommand<TResult>: IRequest<Result<TResult>>
    {
    }
}
