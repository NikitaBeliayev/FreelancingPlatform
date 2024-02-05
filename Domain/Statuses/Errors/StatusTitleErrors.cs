using Shared;


namespace Domain.Statuses.Errors
{
    public static class StatusTitleErrors
    {
        public static Error InvalidName => new ("Objective.ObjectiveStatus.ObjectiveStatusTitle", $"The Title value must be 1", 422);
    }
}
