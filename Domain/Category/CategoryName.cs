using Domain.Category.Errors;
using Domain.Roles;
using Domain.Roles.Errors;
using Shared;

namespace Domain.Category;

public sealed record CategoryName
{
    public string Value { get; }

    private CategoryName(string categoryName)
    {
        Value = categoryName;
    }
    
    public static Result<CategoryName> BuildCategoryName(string? categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            return Result<CategoryName>.Failure(null, CategoryNameError.InvalidNameError);
        }
        return Result<CategoryName>.Success(new CategoryName(categoryName.ToLower()));
    }
}