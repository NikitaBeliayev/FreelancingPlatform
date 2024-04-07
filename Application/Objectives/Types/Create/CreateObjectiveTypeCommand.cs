using System.Windows.Input;
using Application.Abstraction.Messaging;

namespace Application.Objectives.Types.Create;

public record CreateObjectiveTypeCommand(TypeDto TypeDto) : ICommand<TypeDto>
{
    
}