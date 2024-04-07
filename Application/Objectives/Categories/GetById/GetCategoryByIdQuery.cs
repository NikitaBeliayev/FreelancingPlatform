using Application.Abstraction.Messaging;

namespace Application.Objectives.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid CategoryId): IQuery<CategoryDto>
    {
    }
}
