using Application.Abstraction.Messaging;
using Application.Objectives.ResponseDto;
using Application.Models;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
	public record GetAllObjectivesWithPaginationQuery(int PageNum, int PageSize) : IQuery<PaginationModel<ResponseObjectiveDto>>
	{

	}
}
