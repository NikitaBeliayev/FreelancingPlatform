using Domain.CommunicationChannels;
using Domain.CommunicationChannels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class CommunicationChannelRepository : ICommunicationChannelRepository
{
    private readonly AppDbContext _dbContext;
    
    public CommunicationChannelRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void ChangeStateToUnchanged(CommunicationChannel communicationChannel)
    {
        _dbContext.Entry(communicationChannel).State = EntityState.Unchanged;
    }
}