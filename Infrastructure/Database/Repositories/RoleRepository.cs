using Domain.Repositories;
using Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public void ChangeStateToUnchangedForCollection(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
        {
            _dbSet.Entry(role).State = EntityState.Unchanged;
        }
    }
}