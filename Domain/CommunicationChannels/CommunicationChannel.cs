using Domain.UserCommunicationChannels;
using Domain.Users;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannel : Entity
{
    public ICollection<UserCommunicationChannel> UserCommunicationChannels { get; set; } =
        new List<UserCommunicationChannel>();
    public CommunicationChannelType Type { get; set; }

    public CommunicationChannel() : base(Guid.NewGuid())
    {
        
    }

    public CommunicationChannel(Guid id, CommunicationChannelType type, ICollection<UserCommunicationChannel> userCommunicationChannels) : base(id)
    {
        Type = type;
        UserCommunicationChannels = userCommunicationChannels;
    }
}