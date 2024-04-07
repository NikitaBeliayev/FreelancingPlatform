using Domain.UserCommunicationChannels;
using Shared;

namespace Domain.CommunicationChannels;

public class CommunicationChannel : Entity
{
    public ICollection<UserCommunicationChannel> UserCommunicationChannels { get; set; } =
        new List<UserCommunicationChannel>();
    public CommunicationChannelName Name { get; set; }

    public CommunicationChannel(Guid id) : base(id)
    { }

    public CommunicationChannel(Guid id, CommunicationChannelName name, ICollection<UserCommunicationChannel> userCommunicationChannels) : base(id)
    {
        Id = id;
        Name = name;
        UserCommunicationChannels = userCommunicationChannels;
    }
}