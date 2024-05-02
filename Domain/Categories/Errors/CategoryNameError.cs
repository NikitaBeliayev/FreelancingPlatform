using Shared;

namespace Domain.Categories.Errors;

public static class CategoryNameError
{
	public static Error InvalidNameError =>
        new Error("Category.CategoryName.InvalidName", "The category name must not be empty", 422);
}