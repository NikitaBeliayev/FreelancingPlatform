using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
	public record GetAllObjectivesWithPaginationQuery(int Take, int Skip) : IQuery<IEnumerable<ResponseObjectiveDto>>
	{

	}
}
