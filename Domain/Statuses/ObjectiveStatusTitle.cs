using Domain.Statuses.Errors;
using Shared;

namespace Domain.Statuses
{
    public class ObjectiveStatusTitle
    {
        public string Value {  get; }

        private ObjectiveStatusTitle(string objectiveStatus)
        {
            Value = objectiveStatus;
        }

        public static Result<ObjectiveStatusTitle> BuildStatusTitle(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? Result<ObjectiveStatusTitle>.Failure(null, new Error("", "", 500)) 
                : Result<ObjectiveStatusTitle>.Success(new ObjectiveStatusTitle(value));
        }
        
        /// <summary>
        /// Use this method only for ef core configuration
        /// </summary>
        /// <param name="objectiveStatus"></param>
        /// <returns></returns>
        public static Result<ObjectiveStatusTitle> BuildStatusTitleWithoutValidation(string objectiveStatus)
        {
            return Result<ObjectiveStatusTitle>.Success(new ObjectiveStatusTitle(objectiveStatus));
        }
    }
}
