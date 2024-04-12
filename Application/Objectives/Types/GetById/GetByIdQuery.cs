using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.GetById;

public record GetByIdQuery(Guid Id) : IQuery<ResponseTypeDto>
{
    
}