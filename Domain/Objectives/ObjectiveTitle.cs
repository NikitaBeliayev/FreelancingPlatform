using Domain.Objectives.Errors;
using Shared;

namespace Domain.Objectives;

public class ObjectiveTitle
{
    public string Value { get; }

    private ObjectiveTitle(string description)
    {
        Value = description;
    }

    public static Result<ObjectiveTitle> BuildName(string description)
    {
        if (string.IsNullOrEmpty(description))
        {
            return  Result<ObjectiveTitle>.Failure(null, ObjectiveTitleErrors.InvalidName);
        }
        if (description.Length < 6)
        {
            return  Result<ObjectiveTitle>.Failure(null, ObjectiveTitleErrors.InvalidNameLength);
        }
        
        return Result<ObjectiveTitle>.Success(new ObjectiveTitle(description));
    }
}