using Application.Abstraction.Messaging;
using Application.Models;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.Types.GetByIdWithPagination;

public record GetAllObjectiveTypesWithPaginationQuery(int pageNum, int pageSize) : IQuery<PaginationModel<ResponseTypeDto>>
{
    
}