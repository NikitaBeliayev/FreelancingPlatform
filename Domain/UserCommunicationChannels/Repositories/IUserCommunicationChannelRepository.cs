using Domain.CommunicationChannels;
using Domain.Users.UserDetails;

namespace Domain.UserCommunicationChannels.Repositories;

public interface IUserCommunicationChannelRepository
{
    Task<UserCommunicationChannel?> CreateAsync(UserCommunicationChannel communicationChannel, 
        CancellationToken cancellationToken = default);
}