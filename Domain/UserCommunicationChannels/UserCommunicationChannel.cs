using Domain.CommunicationChannels;
using Domain.Users.UserDetails;
using Shared;

namespace Domain.UserCommunicationChannels;

public class UserCommunicationChannel : Entity
{
    public User User { get; set; }
    public Guid UserId { get; set; }
    
    public CommunicationChannel CommunicationChannel { get; set; }
    public int CommunicationChannelId { get; set; }
    public bool IsConfirmed { get; set; }
    public Guid ConfirmationToken { get; set; }

    public UserCommunicationChannel() : base(Guid.NewGuid())
    {
        
    }

    public UserCommunicationChannel(Guid id, User user, Guid userId, bool isConfirmed, Guid confirmationToken, CommunicationChannel communicationChannel,
        int communicationChannelId) : base(id)
    {
        User = user;
        UserId = userId;
        IsConfirmed = isConfirmed;
        ConfirmationToken = confirmationToken;
        CommunicationChannel = communicationChannel;
        CommunicationChannelId = communicationChannelId;
    }
}