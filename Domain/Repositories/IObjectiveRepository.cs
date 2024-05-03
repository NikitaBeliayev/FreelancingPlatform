using Domain.Objectives;

namespace Domain.Repositories;

public interface IObjectiveRepository : IRepository<Objective>
{
    Task<(IEnumerable<Objective>, int)> GetAllForImplementorWithPagination(int take, int skip, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Objective>, int)> GetByCreatorIdWithPagination(Guid creatorId, int take, int skip, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Objective>, int)> GetByImplementorIdWithPagination(Guid implementorId, int take, int skip, CancellationToken cancellationToken = default);

    Task<int> GetTotalCountForImplementorTasks(Guid implementorId, CancellationToken cancellationToken = default);

    Task<int> GetTotalCountForCreator(Guid creatorId, CancellationToken cancellationToken = default);

    Task<int> GetTotalCountForImplementor(CancellationToken cancellationToken = default);
}