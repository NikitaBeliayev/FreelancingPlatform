using Domain.Objectives.Errors;
using Shared;

namespace Domain.Objectives;

public class ObjectiveDescription
{
    public string Value { get; }

    private ObjectiveDescription(string description)
    {
        Value = description;
    }

    public static Result<ObjectiveDescription> BuildName(string description)
    {
        if (string.IsNullOrEmpty(description))
        {
            return  Result<ObjectiveDescription>.Failure(null, ObjectiveDescriptionErrors.InvalidName);
        }
        if (description.Length < 20 || description.Length > 5000)
        {
            return  Result<ObjectiveDescription>.Failure(null, ObjectiveDescriptionErrors.InvalidNameLength);
        }
        
        return Result<ObjectiveDescription>.Success(new ObjectiveDescription(description));
    }
}