using Application.Abstraction.Messaging;
using Application.Objectives.RequestDto;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.CreateObjective;

public record CreateObjectiveCommand(ObjectiveCreateDto RequestDto) : ICommand<SimpleResponseObjectiveDto>;
