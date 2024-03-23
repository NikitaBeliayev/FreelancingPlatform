
using Application.Abstraction.Messaging;

namespace Application.Objectives.Categories.CreateByTitle
{
    public record CreateCategoryByTitleQuery(string title) : IQuery<CategoryDto>
    {
    }
}
