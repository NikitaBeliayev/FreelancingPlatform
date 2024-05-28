using System.Linq.Expressions;
using Domain.Models;
using Shared;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    TEntity Delete(TEntity entity);
    IAsyncEnumerable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    TEntity Update(TEntity entity);
    Task<TEntity?> GetByExpressionWithIncludesAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    Task<GetPaginatedResultModel<TEntity>> GetAllWithIncludesAndPaginationAsync(int take, int skip, 
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    Task<GetPaginatedResultModel<TEntity>> GetByExpressionWithIncludesAndPaginationAsync(Expression<Func<TEntity, bool>> expression, int take, int skip, 
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
}