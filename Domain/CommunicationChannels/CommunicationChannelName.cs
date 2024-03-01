using Domain.CommunicationChannels.Errors;
using Domain.Roles;
using Domain.Roles.Errors;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannelName
{
    public string Value { get; }

    private CommunicationChannelName(CommunicationChannelNameType communicationChannelNameType)
    {
        Value = communicationChannelNameType.ToString();
    }
    
    public static Result<CommunicationChannelName> BuildCommunicationChannelName(int communicationChannelNameType)
    {
        if (communicationChannelNameType is not 1)
        {
            return Result<CommunicationChannelName>.Failure(null, CommunicationChannelNameErrors.InvalidName);
        }

        return Result<CommunicationChannelName>.Success(new CommunicationChannelName(((CommunicationChannelNameType)communicationChannelNameType)));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="communicationChannelName"></param>
    /// <returns></returns>
    public static Result<CommunicationChannelName> BuildCommunicationChannelNameWithoutValidation(int communicationChannelName)
    {
        return Result<CommunicationChannelName>.Success(new CommunicationChannelName(((CommunicationChannelNameType)communicationChannelName)));
    }
}