
using System.Collections.Immutable;
using Shared;

namespace Domain.Statuses;

public class ObjectiveStatusTitleVariations
{
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _objectiveStatusTitleCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
        new KeyValuePair<Guid, string>(new Guid("2f2f54aa-46dd-29d0-6459-2afdb5e950ee"), "WaitingForApproval"),
        new KeyValuePair<Guid, string>(new Guid("327db9d4-0282-c319-b047-dcf22483e225"), "WaitingForAssignment"),
        new KeyValuePair<Guid, string>(new Guid("6cb13af0-83d5-c772-7ba4-5a3d9a5a1cb9"), "Draft"),
        new KeyValuePair<Guid, string>(new Guid("c9b0e0b6-fb0c-fedd-767f-137f8066d1df"), "InProgress"),
        new KeyValuePair<Guid, string>(new Guid("e26529f9-a7c8-b3af-c1b9-a5c09a263636"), "Done")
    );
    
    public static Guid WaitingForApproval => _objectiveStatusTitleCollection.FirstOrDefault(pair => pair.Value == "WaitingForApproval")!.Key;
    public static Guid WaitingForAssignment => _objectiveStatusTitleCollection.FirstOrDefault(pair => pair.Value == "WaitingForAssignment")!.Key;
    public static Guid Draft => _objectiveStatusTitleCollection.FirstOrDefault(pair => pair.Value == "Draft")!.Key;
    public static Guid InProgress => _objectiveStatusTitleCollection.FirstOrDefault(pair => pair.Value == "InProgress")!.Key;
    public static Guid Done => _objectiveStatusTitleCollection.FirstOrDefault(pair => pair.Value == "Done")!.Key;

    public static Result<string> GetValue(Guid key)
    {
        var possibleElement = _objectiveStatusTitleCollection.FirstOrDefault(element => element.Key == key);

        return Result<string>.Success(possibleElement.Value);
    }
}