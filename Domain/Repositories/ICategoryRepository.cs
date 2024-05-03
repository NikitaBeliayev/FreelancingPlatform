using Domain.Categories;
using Shared;
using System.Linq.Expressions;

namespace Domain.Repositories

{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<(IAsyncEnumerable<Category>, int)> GetByTitleWithPagination(int take, int skip, CancellationToken cancellationToken = default);

        public Category? GetByTitle(string title, CancellationToken cancellationToken = default);
    }
}
