using Application.Abstraction.Messaging;

namespace Application.Objectives.CreateObjective;

public record CreateObjectiveCommand(ObjectiveDto RequestDto) : ICommand<ObjectiveDto>;
