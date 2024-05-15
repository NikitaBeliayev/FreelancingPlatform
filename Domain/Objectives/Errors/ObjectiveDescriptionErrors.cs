using Shared;

namespace Domain.Objectives.Errors;

public static class ObjectiveDescriptionErrors
{
    public static Error InvalidName =>
        new("Objectives.ObjectiveDescription.InvalidName", $"The description must not be empty", 422);
    public static Error InvalidNameLength =>
        new("Objectives.ObjectiveDescription.InvalidNameLength", $"The length of the description should be between 10 and 5000 characters", 
            422);
}