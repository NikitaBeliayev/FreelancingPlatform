using Shared;

namespace Domain.Objectives.ObjectiveTypes.Errors;

public static class ObjectiveTypeErrors
{
	public static Error InvalidTitle =>
		new("Objectives.ObjectiveTypes.RoleName.ObjectiveTypeTitle", $"The Name value must be either 1, 2, or 3", 422);
}


