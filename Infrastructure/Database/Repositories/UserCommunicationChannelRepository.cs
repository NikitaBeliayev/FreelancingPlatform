using System.Linq.Expressions;
using System.Xml.Serialization;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserCommunicationChannelRepository : IUserCommunicationChannelRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserCommunicationChannelRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    
    public async Task<UserCommunicationChannel?> CreateAsync(UserCommunicationChannel userCommunicationChannel, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserCommunicationChannels.AddAsync(userCommunicationChannel, cancellationToken);
        return userCommunicationChannel;
    }

    public UserCommunicationChannel Delete(UserCommunicationChannel entity)
    {
        return _dbContext.UserCommunicationChannels.Remove(entity).Entity;
    }

    IAsyncEnumerable<UserCommunicationChannel> IRepository<UserCommunicationChannel>.GetAll()
    {
        return _dbContext.UserCommunicationChannels.AsAsyncEnumerable();
    }

    public async Task<UserCommunicationChannel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserCommunicationChannels.FindAsync(new object[] { id }, cancellationToken);
    }
    public UserCommunicationChannel Update(UserCommunicationChannel entity)
    {
        return _dbContext.UserCommunicationChannels.Update(entity).Entity;
    }

    public async Task<UserCommunicationChannel?> GetByExpressionWithIncludesAsync(Expression<Func<UserCommunicationChannel, bool>> expression, CancellationToken cancellationToken = default,
        params Expression<Func<UserCommunicationChannel, object>>[] includes)
    {
        return await includes.Aggregate(_dbContext.UserCommunicationChannels.AsQueryable(), (c, p) => c.Include(p)).FirstOrDefaultAsync(expression, cancellationToken);
    }
}