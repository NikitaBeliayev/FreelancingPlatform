using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.Delete;

public record DeleteObjectiveTypeCommand(Guid Id) : ICommand<ResponseTypeDto>
{
    
}