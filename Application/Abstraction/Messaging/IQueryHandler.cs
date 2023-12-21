using MediatR;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Messaging
{
    public interface IQueryHandler<TQuery, TResult>: IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult>
    {
    }
}
