using Shared;

namespace Domain.Objectives.Errors;

public static class ObjectiveTitleErrors
{
    public static Error InvalidName =>
        new("Roles.RoleName.InvalidName", $"The objective title should not be empty", 422);
    public static Error InvalidNameLength =>
        new("Roles.RoleName.InvalidName", $"The length of the objective title should be at least 10 characters", 422);
}