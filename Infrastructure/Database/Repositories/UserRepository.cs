using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<User?> CreateAsync(User entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(entity, cancellationToken);

            return entity;
        }

        public User Delete(User entity)
        {
            return _dbContext.Users.Remove(entity).Entity;
        }
        public IAsyncEnumerable<User> GetAll()
        {
            return _dbContext.Users.AsAsyncEnumerable();
        }

        public User Update(User entity)
        {
            return _dbContext.Users.Update(entity).Entity;
        }

        public async Task<User?> GetByExpressionWithIncludesAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default,
            params Expression<Func<User, object>>[] includes)
        {
            return await includes.Aggregate(_dbContext.Users.AsQueryable(), (c, p) => c.Include(p)).
                FirstOrDefaultAsync(expression, cancellationToken);
        }
    }
}
