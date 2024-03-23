using Application.Abstraction.Messaging;
using Application.Objectives.Categories.RequestDto;

namespace Application.Objectives.Categories.GetByTitle
{
    public record GetCategoryByTitleWithPaginationQuery(CategorySearchDto SearchParams) : IQuery<List<CategoryDto>>
    {
    }
}
