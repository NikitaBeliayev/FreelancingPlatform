using Application.Abstraction.Messaging;
using Application.Objectives.Types.RequestDto;

namespace Application.Objectives.Types.Update;

public record UpdateObjectiveTypeCommand(UpdateObjectiveTypeRequestDto RequestDto) : ICommand<TypeDto>
{
    
}