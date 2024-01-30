namespace Domain.Roles.Repositories;

public interface IRoleRepository
{
    void ChangeStateToUnchangedForCollection(IEnumerable<Role> roles); 
}