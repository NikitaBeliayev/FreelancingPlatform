using Domain.Objectives.Errors;
using Shared;

namespace Domain.Objectives;

public class ObjectiveDeadline
{
    public DateTime Value { get; }

    private ObjectiveDeadline(DateTime value)
    {
        Value = value;
    }

    public static Result<ObjectiveDeadline> BuildName(DateTime value)
    {
        return value < DateTime.Now.AddDays(1)
            ? Result<ObjectiveDeadline>.Failure(null,
                new Error(typeof(ObjectiveDeadline).Namespace!, "ETA must be at least +1 day from now", 422))
            : Result<ObjectiveDeadline>.Success(new ObjectiveDeadline(value));
    }

    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public static Result<ObjectiveDeadline> BuildNameWithoutValidation(DateTime value)
    {
        return Result<ObjectiveDeadline>.Success(new ObjectiveDeadline(value));
    }
}