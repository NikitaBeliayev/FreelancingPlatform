using System.Linq.Expressions;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Database.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly AppDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
    }

    public TEntity Delete(TEntity entity)
    {
        return _dbSet.Remove(entity).Entity;
    }

    public IAsyncEnumerable<TEntity> GetAll()
    {
        return _dbSet.AsAsyncEnumerable();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        return _dbSet.Update(entity).Entity;
    }

    public async Task<TEntity?> GetByExpressionWithIncludesAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        return await includes.Aggregate(_dbSet.AsQueryable(), (c, p) => c.Include(p)).
            FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<(IAsyncEnumerable<TEntity>, int)> GetAllWithPagination(int take, int skip, CancellationToken cancellationToken = default)
    {
        return (_dbSet.Skip(skip).Take(take).AsAsyncEnumerable(), await _dbSet.CountAsync());
    }
}