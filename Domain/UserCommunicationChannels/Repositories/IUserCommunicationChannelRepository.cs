using System.Linq.Expressions;
using Domain.CommunicationChannels;
using Domain.Users.UserDetails;

namespace Domain.UserCommunicationChannels.Repositories;

public interface IUserCommunicationChannelRepository
{
    Task<UserCommunicationChannel?> CreateAsync(UserCommunicationChannel communicationChannel, 
        CancellationToken cancellationToken = default);
    Task<UserCommunicationChannel?> GetByExpressionWithIncludesAsync(Expression<Func<UserCommunicationChannel, bool>> expression,
        CancellationToken cancellationToken = default, params Expression<Func<UserCommunicationChannel, object>>[] includes);
}