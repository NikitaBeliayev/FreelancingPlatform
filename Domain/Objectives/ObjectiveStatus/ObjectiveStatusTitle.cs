using Shared;

namespace Domain.Objectives.ObjectiveStatus
{
    public class ObjectiveStatusTitle
    {
        public string Value {  get; }

        private ObjectiveStatusTitle(ObjectiveStatusTitleType statusType)
        {
            Value = statusType.ToString();
        }

        public static Result<ObjectiveStatusTitle> BuildStatusTitle(int objectiveStatus)
        {
            if (objectiveStatus != 1)
            {
                return Result<ObjectiveStatusTitle>.Failure(null, ObjectiveStatusTitleErrors.InvalidName);
            }

            return Result<ObjectiveStatusTitle>.Success(new ObjectiveStatusTitle((ObjectiveStatusTitleType)objectiveStatus));
        }
    }
}
