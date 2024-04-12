using System.Collections.Immutable;
using Shared;

namespace Domain.Types;

public static class ObjectiveTypeVariations
{
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _typeCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
            new KeyValuePair<Guid, string>(new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"), "Individual"),
            new KeyValuePair<Guid, string>(new Guid("34719303-dace-07b9-8be3-9a77ee3a48a0"), "Group"),
            new KeyValuePair<Guid, string>(new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"), "Team")
        );
    
    public static Guid Individual => _typeCollection.FirstOrDefault(pair => pair.Value == "Individual")!.Key;
    public static Guid Group => _typeCollection.FirstOrDefault(pair => pair.Value == "Group")!.Key;
    public static Guid Team => _typeCollection.FirstOrDefault(pair => pair.Value == "Team")!.Key;
    
    public static Result<string> GetValue(Guid key)
    {
        var possibleElement = _typeCollection.FirstOrDefault(element => element.Key == key);
        
        return Result<string>.Success(possibleElement.Value);
    }
}
