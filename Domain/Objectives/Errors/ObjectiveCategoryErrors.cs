using Shared;

namespace Domain.Objectives.Errors;

public static class ObjectiveCategoryErrors
{
	public static Error NoTagsProvided =>
		new("Objectives.Categories", $"At least 1 Tag is mandatory", 422);
}
