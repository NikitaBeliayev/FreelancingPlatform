using Application.Abstraction.Messaging;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
	public record GetAllObjectivesWithPaginationQuery(int PageNum, int PageSize) : IQuery<PaginatedResultDto<ResponseObjectiveDto>>
	{

	}
}
