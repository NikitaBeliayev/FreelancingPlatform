using System.Windows.Input;
using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.Create;

public record CreateObjectiveTypeCommand(TypeDto TypeDto) : ICommand<ResponseTypeDto>
{
    
}