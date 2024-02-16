using Domain.Roles;

namespace Domain.Repositories;

public interface IRoleRepository
{
    void ChangeStateToUnchangedForCollection(IEnumerable<Role> roles); 
}