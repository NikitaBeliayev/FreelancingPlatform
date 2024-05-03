using Application.Abstraction.Messaging;
using Application.Models;

namespace Application.Objectives.Categories.GetByTitle
{
    public record GetByTitleWithPaginationQuery(int pageNum, int pageSize) : IQuery<PaginationModel<CategoryDto>>
    {
    }
}
