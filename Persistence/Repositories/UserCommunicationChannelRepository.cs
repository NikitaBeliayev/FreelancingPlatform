using Domain.CommunicationChannels;
using Domain.UserCommunicationChannels;
using Domain.UserCommunicationChannels.Repositories;
using Domain.Users.UserDetails;

namespace Persistence.Repositories;

public class UserCommunicationChannelRepository : IUserCommunicationChannelRepository
{
    private readonly AppDbContext _dbContext;
    public UserCommunicationChannelRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    public async Task<UserCommunicationChannel?> CreateAsync(UserCommunicationChannel userCommunicationChannel, CancellationToken cancellationToken)
    {
        await _dbContext.UserCommunicationChannels.AddAsync(userCommunicationChannel, cancellationToken);
        return userCommunicationChannel;
    }
}