
using Application.Abstraction.Messaging;

namespace Application.Objectives.Categories.CreateByTitle
{
    public record CreateCategoryCommand(string title) : IQuery<CategoryDto>
    {
    }
}
