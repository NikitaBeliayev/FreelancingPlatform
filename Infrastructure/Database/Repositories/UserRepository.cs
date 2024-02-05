using Domain.Roles;
using Domain.Users.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);

            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>()
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
        
        public async Task<User?> GetUserByAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(expression, cancellationToken);
        }


        public async Task<User?> GetByExpressionWithCommunicationChannelsAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<User>().Include(u => u.CommunicationChannels).ThenInclude(c => c.CommunicationChannel).FirstOrDefaultAsync(expression, cancellationToken);
        }
    }
}
