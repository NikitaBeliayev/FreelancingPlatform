using Domain.Categories;
using Shared;
using System.Linq.Expressions;

namespace Domain.Repositories

{
    public interface ICategoryRepository : IRepository<Category>
    {
        IAsyncEnumerable<Category> GetByTitleWithPagination(Func<Category, bool> expression, int take, int pageSize, 
            CancellationToken cancellationToken = default);

        public Category? GetByTitle(string title, CancellationToken cancellationToken = default);
    }
}
