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
        if (description.Length < 10)
        {
            return  Result<ObjectiveTitle>.Failure(null, ObjectiveTitleErrors.InvalidNameLength);
        }
        
        return Result<ObjectiveTitle>.Success(new ObjectiveTitle(description));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public static Result<ObjectiveTitle> BuildNameWithoutValidation(string description)
    {
        return Result<ObjectiveTitle>.Success(new ObjectiveTitle(description));
    }
}