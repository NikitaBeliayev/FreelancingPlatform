using System.Linq.Expressions;
using Domain.CommunicationChannels;
using Domain.UserCommunicationChannels;
using Domain.UserCommunicationChannels.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

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

    public async Task<UserCommunicationChannel?> GetByExpressionWithIncludesAsync(Expression<Func<UserCommunicationChannel, bool>> expression, CancellationToken cancellationToken = default,
        params Expression<Func<UserCommunicationChannel, object>>[] includes)
    {
        return await includes.Aggregate(_dbContext.Set<UserCommunicationChannel>().AsQueryable(), (c, p) => c.Include(p)).FirstOrDefaultAsync(expression, cancellationToken);
    }
}