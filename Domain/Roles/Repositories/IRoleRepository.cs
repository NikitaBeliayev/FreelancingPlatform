namespace Domain.Roles.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ICollection<Role>> GetRolesByNameCollectionAsync(IEnumerable<RoleName> idCollection, CancellationToken cancellationToken);
}