using Domain.CommunicationChannels;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class CommunicationChannelRepository : Repository<CommunicationChannel>, ICommunicationChannelRepository
{
    public CommunicationChannelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public void ChangeStateToUnchanged(CommunicationChannel communicationChannel)
    {
        _dbSet.Entry(communicationChannel).State = EntityState.Unchanged;
    }
}