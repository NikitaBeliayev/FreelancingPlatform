using Application.Abstraction.Messaging;

namespace Application.Objectives.Types.GetById;

public record GetByIdQuery(Guid Id) : IQuery<TypeDto>
{
    
}