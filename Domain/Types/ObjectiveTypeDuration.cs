using Shared;

namespace Domain.Types;

public class ObjectiveTypeDuration
{
    private ObjectiveTypeDuration(int duration)
    {
        Duration = duration;
    }

    public int Duration { get; }

    public static Result<ObjectiveTypeDuration> BuildObjectiveTypeDuration(int duration)
    {
        return duration < 8
            ? Result<ObjectiveTypeDuration>.Failure(null,
                new Error(typeof(ObjectiveTypeDuration).Namespace!, "Duration must be more or equal to 8 hours", 500))
            : Result<ObjectiveTypeDuration>.Success(new ObjectiveTypeDuration(duration));
    }

    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="objectiveType"></param>
    /// <returns></returns>
    public static Result<ObjectiveTypeDuration> BuildObjectiveTypeTitleWithoutValidation(int duration)
    {
        return Result<ObjectiveTypeDuration>.Success(new ObjectiveTypeDuration(duration));
    }
}