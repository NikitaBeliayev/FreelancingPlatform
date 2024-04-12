using Application.Abstraction.Messaging;
using Application.Objectives.Types.RequestDto;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.Update;

public record UpdateObjectiveTypeCommand(UpdateObjectiveTypeRequestDto RequestDto) : ICommand<ResponseTypeDto>
{
    
}