using Shared;


namespace Domain.Objectives.ObjectiveStatus
{
    public static class ObjectiveStatusTitleErrors
    {
        public static Error InvalidName => new ("Objective.ObjectiveStatus.ObjectiveStatusTitle", $"The Title value must be 1", 422);
    }
}
