using Application.Roles;
using Domain.Roles;
using Domain.Roles.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void ChangeStateToUnchangedForCollection(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
        {
            _dbContext.Role.Entry(role).State = EntityState.Unchanged;
        }
    }
}