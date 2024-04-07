using System.Linq.Expressions;
using Domain.Users;
using Domain.Users.UserDetails;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
