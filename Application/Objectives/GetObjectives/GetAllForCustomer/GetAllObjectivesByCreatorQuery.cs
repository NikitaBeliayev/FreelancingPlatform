using Application.Abstraction.Messaging;
using Application.Models;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public record GetAllObjectivesByCreatorQuery(Guid CreatorId, int PageNum, int PageSize) : IQuery<PaginationModel<ResponseObjectiveDto>>
    {
    }
}
