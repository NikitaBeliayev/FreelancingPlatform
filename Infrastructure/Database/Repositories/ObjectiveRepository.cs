using System.Linq.Expressions;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : IObjectiveRepository
{
    private readonly AppDbContext _dbContext;
    
    public ObjectiveRepository(AppDbContext context)
    {
        _dbContext = context;
    }
    public async Task<Objective?> CreateAsync(Objective entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Objectives.AddAsync(entity, cancellationToken);
        return entity;
    }
    public IAsyncEnumerable<Objective> GetAll()
    {
        return _dbContext.Objectives.AsAsyncEnumerable();
    }

    public async Task<Objective?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Objectives.FindAsync(new object[] { id }, cancellationToken);
    }

    public Objective Delete(Objective entity)
    {
        return _dbContext.Objectives.Remove(entity).Entity;
    }
    public Objective Update(Objective entity)
    {
        return _dbContext.Objectives.Update(entity).Entity;
    }

    public async Task<Objective?> GetByExpressionWithIncludesAsync(Expression<Func<Objective, bool>> expression, CancellationToken cancellationToken = default,
        params Expression<Func<Objective, object>>[] includes)
    {
        return await includes.Aggregate(_dbContext.Objectives.AsQueryable(), (c, p) => c.Include(p)).FirstOrDefaultAsync(expression, cancellationToken);
    }
}