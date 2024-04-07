using System.Collections.Immutable;
using Shared;

namespace Domain.Types;

public static class ObjectiveTypeVariations
{
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _typeCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
            new KeyValuePair<Guid, string>(new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), "Individual"),
            new KeyValuePair<Guid, string>(new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), "Group"),
            new KeyValuePair<Guid, string>(new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), "Team")
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
