using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.GetByIdWithPagination;

public record GetAllObjectiveTypesWithPaginationQuery(int Take, int Skip) : IQuery<IEnumerable<ResponseTypeDto>> 
{
    
}