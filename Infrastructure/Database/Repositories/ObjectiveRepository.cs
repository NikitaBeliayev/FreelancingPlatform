using System.Linq.Expressions;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
{
    public ObjectiveRepository(AppDbContext dbContext) : base(dbContext)
    { 
    }

	public async Task<IEnumerable<Objective>> GetAllForImplementorWithPagination(int take, int skip, CancellationToken cancellationToken = default)
	{
		return await _dbSet
            .Include(o => o.Categories)
			.Include(o => o.Creator)
			.Include(o => o.Type)
			.Where(x => x.ObjectiveStatusId.ToString() == "2f2f54aa-46dd-29d0-6459-2afdb5e950ee" || x.ObjectiveStatusId.ToString() == "327db9d4-0282-c319-b047-dcf22483e225")
			.Skip(skip).Take(take).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Objective>> GetByCreatorIdWithPagination(Guid creatorId, int take, int skip, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(o => o.Categories)
            .Include(o => o.Creator)
            .Include(o => o.Type)
            .Where(o => o.CreatorId == creatorId)
            .Skip(skip).Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Objective>> GetByImplementorIdWithPagination(Guid implementorId, int take, int skip, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(o => o.Categories)
            .Include(o => o.Creator)
            .Include(o => o.Type)
            .Where(o => o.Implementors.Any(i => i.Id == implementorId))
            .Skip(skip).Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetTotalCountForImplementorTasks(Guid implementorId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(o => o.Implementors.Any(i => i.Id == implementorId))
            .CountAsync(cancellationToken);
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