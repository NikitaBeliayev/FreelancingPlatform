using System.Linq.Expressions;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

    public async Task<GetPaginatedResultModel<TEntity>> GetAllWithIncludesAndPaginationAsync(int take, int skip, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var result = includes.Aggregate(_dbSet.AsQueryable(), (c, p) => c.Include(p));
        var count = await result.CountAsync(cancellationToken: cancellationToken);

        return new GetPaginatedResultModel<TEntity>() { result = await result.Skip((skip - 1) * take).Take(take).ToListAsync(cancellationToken), count = count };
    }

    public async Task<GetPaginatedResultModel<TEntity>> GetByExpressionWithIncludesAndPaginationAsync(Expression<Func<TEntity, bool>> expression, 
        int take, int skip, 
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var result = includes.Aggregate(_dbSet.AsQueryable(), (c, p) => c.Include(p)).Where(expression);
        var count = await result.CountAsync(cancellationToken: cancellationToken);
        return new GetPaginatedResultModel<TEntity>() { result = await result.Skip((skip - 1) * take).Take(take).ToListAsync(cancellationToken), count = count };
    }

    public async Task<int> GetTotalCountByExpression (Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(expression).CountAsync(cancellationToken: cancellationToken);
    }
}