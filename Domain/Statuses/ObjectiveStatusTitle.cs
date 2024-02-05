using Domain.Statuses.Errors;
using Shared;

namespace Domain.Statuses
{
    public class ObjectiveStatusTitle
    {
        public string Value {  get; }

        private ObjectiveStatusTitle(ObjectiveStatusTitleType objectiveStatusType)
        {
            Value = objectiveStatusType.ToString();
        }

        public static Result<ObjectiveStatusTitle> BuildStatusTitle(int objectiveStatus)
        {
            if (objectiveStatus != 1)
            {
                return Result<ObjectiveStatusTitle>.Failure(null, StatusTitleErrors.InvalidName);
            }

            return Result<ObjectiveStatusTitle>.Success(new ObjectiveStatusTitle((ObjectiveStatusTitleType)objectiveStatus));
        }
    }
}
