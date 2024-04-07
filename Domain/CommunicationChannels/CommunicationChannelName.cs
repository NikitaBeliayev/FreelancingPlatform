using Domain.CommunicationChannels.Errors;
using Domain.Roles;
using Domain.Roles.Errors;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannelName
{
    public string Value { get; }

    private CommunicationChannelName(string communicationChannelNameVariations)
    {
        Value = communicationChannelNameVariations;
    }
    
    public static Result<CommunicationChannelName> BuildCommunicationChannelName(string value)
    {
        
        return string.IsNullOrWhiteSpace(value) ? Result<CommunicationChannelName>.Failure(null, new Error("", "", 500)) 
            : Result<CommunicationChannelName>.Success(new CommunicationChannelName(value));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="communicationChannelName"></param>
    /// <returns></returns>
    public static Result<CommunicationChannelName> BuildCommunicationChannelNameWithoutValidation(string communicationChannelName)
    {
        return Result<CommunicationChannelName>.Success(new CommunicationChannelName(communicationChannelName));
    }
}