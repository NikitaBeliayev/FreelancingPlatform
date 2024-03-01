using Domain.UserCommunicationChannels;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannel
{
    public int Id { get; set; }
    public ICollection<UserCommunicationChannel> UserCommunicationChannels { get; set; } =
        new List<UserCommunicationChannel>();
    public CommunicationChannelName Name { get; set; }

    public CommunicationChannel()
    {
        
    }

    public CommunicationChannel(int id, CommunicationChannelName name, ICollection<UserCommunicationChannel> userCommunicationChannels)
    {
        Id = id;
        Name = name;
        UserCommunicationChannels = userCommunicationChannels;
    }
}