using Domain.Users.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> CreateAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);

            return user;
        }

        public async Task<User?> GetUserByAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
    }
}
