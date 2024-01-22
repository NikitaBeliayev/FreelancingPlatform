using Domain.CommunicationChannels;
using Domain.CommunicationChannels.Repositories;

namespace Infrastructure.Database.Repositories;

public class CommunicationChannelRepository : ICommunicationChannelRepository
{
    private readonly AppDbContext _dbContext;
    
    public CommunicationChannelRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CommunicationChannel> GetCommunicationChannelByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.CommunicationChannels.FindAsync(id, cancellationToken);
    }
}