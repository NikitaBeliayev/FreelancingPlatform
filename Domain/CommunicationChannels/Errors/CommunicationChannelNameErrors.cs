using Shared;

namespace Domain.CommunicationChannels.Errors;

public static class CommunicationChannelNameErrors
{
    public static Error InvalidName =>
        new("CommunicationChannels.CommunicationChannelName.InvalidName", $"Entity of CommunicationChannel class can only be email", 422);
}