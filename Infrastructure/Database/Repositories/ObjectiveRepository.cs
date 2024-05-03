using Domain.Objectives;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
{
    public ObjectiveRepository(AppDbContext dbContext) : base(dbContext)
    { 
    }

	public async Task<(IEnumerable<Objective>, int)> GetAllForImplementorWithPagination(int take, int skip, CancellationToken cancellationToken = default)
	{
        var objectives = _dbSet
            .Include(o => o.Categories)
            .Include(o => o.Creator)
            .Include(o => o.Type)
            .Where(x => x.ObjectiveStatusId.ToString() == "2f2f54aa-46dd-29d0-6459-2afdb5e950ee" || x.ObjectiveStatusId.ToString() == "327db9d4-0282-c319-b047-dcf22483e225");
        int total = await objectives.CountAsync();
        var result = await objectives.Skip(skip).Take(take).ToListAsync(cancellationToken);

        return (result, total);
    }

    public async Task<(IEnumerable<Objective>, int)> GetByCreatorIdWithPagination(Guid creatorId, int take, int skip, CancellationToken cancellationToken = default)
    {
        var objectives = _dbSet
            .Include(o => o.Categories)
            .Include(o => o.Creator)
            .Include(o => o.Type)
            .Where(o => o.CreatorId == creatorId);
        int total = await objectives.CountAsync();
        var result = await objectives.Skip(skip).Take(take).ToListAsync(cancellationToken);

        return (result, total);
    }

    public async Task<int> GetTotalCountForCreator(Guid creatorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.CreatorId == creatorId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountForImplementor(CancellationToken cancellationToken = default)
	{
		return await _dbSet
			.Where(x => x.ObjectiveStatusId.ToString() == "2f2f54aa-46dd-29d0-6459-2afdb5e950ee" || x.ObjectiveStatusId.ToString() == "327db9d4-0282-c319-b047-dcf22483e225")
			.CountAsync(cancellationToken);
	}
}