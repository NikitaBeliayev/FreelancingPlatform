using Application.Abstraction.Messaging;

namespace Application.Objectives.Types.Delete;

public record DeleteObjectiveTypeCommand(Guid Id) : ICommand<TypeDto>
{
    
}