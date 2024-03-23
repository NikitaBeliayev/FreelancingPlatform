using Application.Abstraction.Messaging;

namespace Application.Objectives.Categories.GetById
{
    public record GetCategoryByIdQuery(int CategoryId): IQuery<CategoryDto>
    {
    }
}
