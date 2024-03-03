using Shared;

namespace Domain.UserCommunicationChannels.Errors;

public static class UserCommunicationChannelErrors
{
    public static Error UserCommunicationChannelNotFound(Guid token) => 
        new("UserCommunicationChannels.UserCommunicationChannelNotFound", $"Entity of UserCommunicationChannel with token:{token} is not found", 
            400);
}