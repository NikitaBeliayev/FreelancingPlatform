using Domain.Roles;

namespace Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    void ChangeStateToUnchangedForCollection(IEnumerable<Role> roles); 
}