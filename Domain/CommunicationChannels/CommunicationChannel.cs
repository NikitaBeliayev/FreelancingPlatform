using Domain.UserCommunicationChannels;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannel
{
    public int Id { get; set; }
    public ICollection<UserCommunicationChannel> UserCommunicationChannels { get; set; } =
        new List<UserCommunicationChannel>();
    public CommunicationChannelType Type { get; set; }

    public CommunicationChannel()
    {
        
    }

    public CommunicationChannel(int id, CommunicationChannelType type, ICollection<UserCommunicationChannel> userCommunicationChannels)
    {
        Id = id;
        Type = type;
        UserCommunicationChannels = userCommunicationChannels;
    }
}