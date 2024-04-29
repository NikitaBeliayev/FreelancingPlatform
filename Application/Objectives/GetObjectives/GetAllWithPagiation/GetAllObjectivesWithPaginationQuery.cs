using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
	public record GetAllObjectivesWithPaginationQuery(int Take, int Skip) : IQuery<IEnumerable<ResponseObjectiveDto>>
	{

	}
}
