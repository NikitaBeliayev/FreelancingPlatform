using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Repositories;
using Domain.Users;

namespace Infrastructure.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
