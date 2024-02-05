namespace Domain.Objectives;

public interface IObjectiveRepository
{
    Task<Objective?> GetObjectiveByIdAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<Objective>?> GetObjectiveByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default);
    
    Task<Objective?> DeleteObjectiveAsync(Guid id,
        CancellationToken cancellationToken = default);

    Objective UpdateObjectiveAsync(Objective objective,
        CancellationToken cancellationToken = default);
}