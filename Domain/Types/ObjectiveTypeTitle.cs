using Shared;

namespace Domain.Types;

public sealed class ObjectiveTypeTitle
{
    private ObjectiveTypeTitle(string objectiveType)
    {
        Title = objectiveType;
    }

    public string Title { get; }

    public static Result<ObjectiveTypeTitle> BuildObjectiveTypeTitle(Guid value)
    {
        return value != ObjectiveTypeVariations.Team && value != ObjectiveTypeVariations.Individual
            ? Result<ObjectiveTypeTitle>.Failure(null,
                new Error(typeof(ObjectiveTypeTitle).Namespace, "Task type should be team or individual", 422))
            : Result<ObjectiveTypeTitle>.Success(
                new ObjectiveTypeTitle(ObjectiveTypeVariations.GetValue(value).Value!));
    }

    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="objectiveType"></param>
    /// <returns></returns>
    public static Result<ObjectiveTypeTitle> BuildObjectiveTypeTitleWithoutValidation(string objectiveType)
    {
        return Result<ObjectiveTypeTitle>.Success(new ObjectiveTypeTitle(objectiveType));
    }
}