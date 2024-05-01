using Application.Abstraction.Messaging;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public record GetAllObjectivesByCreatorQuery(Guid CreatorId, int PageNum, int PageSize) : IQuery<PaginatedResultDto<ResponseObjectiveDto>>
    {
    }
}
