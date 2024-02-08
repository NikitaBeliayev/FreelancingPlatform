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
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="objectiveStatus"></param>
        /// <returns></returns>
        public static Result<ObjectiveStatusTitle> BuildStatusTitleWithoutValidation(int objectiveStatus)
        {
            return Result<ObjectiveStatusTitle>.Success(new ObjectiveStatusTitle((ObjectiveStatusTitleType)objectiveStatus));
        }
    }
}
