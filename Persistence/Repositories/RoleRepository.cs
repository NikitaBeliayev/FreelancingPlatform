using Domain.Roles;
using Domain.Roles.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Role.FindAsync(id, cancellationToken);
    }

    public async Task<ICollection<Role>> GetRolesByNameCollectionAsync(IEnumerable<RoleName> idCollection, CancellationToken cancellationToken)
    {
        return await _dbContext.Role.Where(role => idCollection.Contains(role.Name)).ToListAsync(cancellationToken);
    }
}