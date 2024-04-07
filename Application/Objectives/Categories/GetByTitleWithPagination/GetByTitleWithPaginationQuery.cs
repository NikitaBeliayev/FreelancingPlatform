using Application.Abstraction.Messaging;
using Application.Objectives.Categories.RequestDto;

namespace Application.Objectives.Categories.GetByTitle
{
    public record GetByTitleWithPaginationQuery(CategorySearchDto SearchParams) : IQuery<List<CategoryDto>>
    {
    }
}
