using Application.Abstraction.Messaging;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAssignedTasksForImplementor
{
    public record GetAssignedTasksForImplementorQuery(Guid ImplementorId, int PageNum, int PageSize) : IQuery<PaginatedResultDto<ResponseObjectiveDto>>
    {
    }
}
