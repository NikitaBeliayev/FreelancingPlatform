using Application.Abstraction.Messaging;
using Application.Models;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAssignedTasksForImplementor
{
    public record GetAssignedTasksForImplementorQuery(Guid ImplementorId, int PageNum, int PageSize) : IQuery<PaginationModel<ResponseObjectiveDto>>
    {
    }
}
