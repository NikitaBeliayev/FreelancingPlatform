using Shared;

namespace Domain.Categories.Errors;

public static class CategoryErrors
{
	public static Error NotFound(Guid id) => new("Categories.NotFound", $"The category with id {id} has not been found", 404);
}
