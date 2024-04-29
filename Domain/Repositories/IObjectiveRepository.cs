using Domain.Objectives;
using Shared;

namespace Domain.Repositories;

public interface IObjectiveRepository : IRepository<Objective>
{
    Task<IEnumerable<Objective>> GetAllForImplementorWithPagination(int take, int skip, CancellationToken cancellationToken = default);

    Task<IEnumerable<Objective>> GetByCreatorId(Guid creatorId, CancellationToken cancellationToken = default);
}