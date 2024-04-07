using System.Collections.Immutable;
using Shared;

namespace Domain.CommunicationChannels;

public static class CommunicationChannelNameVariations
{
    
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _communicationChannelCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
        new KeyValuePair<Guid, string>(new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), "Email")
    );
    
    public static Guid Email => _communicationChannelCollection.FirstOrDefault(pair => pair.Value == "Email")!.Key;

    public static Result<string> GetValue(Guid key)
    {
        var possibleElement = _communicationChannelCollection.FirstOrDefault(element => element.Key == key);

        return Result<string>.Success(possibleElement.Value);
    }
}