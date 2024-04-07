using System.Linq.Expressions;
using System.Xml.Serialization;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class UserCommunicationChannelRepository : Repository<UserCommunicationChannel>, IUserCommunicationChannelRepository
{
    public UserCommunicationChannelRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
}