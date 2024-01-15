using System.Linq.Expressions;
using Domain.Users.UserDetails;

namespace Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<User?> CreateAsync(User user);
        Task<User?> GetUserByAsync(Expression<Func<User, bool>> expression,
            CancellationToken cancellationToken = default);
    }
}
