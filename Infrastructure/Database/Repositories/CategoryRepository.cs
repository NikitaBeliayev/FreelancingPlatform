using System.Linq.Expressions;
using Domain.Categories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
           
        }

        public async Task<(IAsyncEnumerable<Category>, int)> GetByTitleWithPagination(int take, int skip, CancellationToken cancellationToken = default)
        {
            var categories = _dbSet.OrderBy(x => x.Objectives.Count);
            int total = await categories.CountAsync();
            var results = categories.Skip(skip).Take(take).AsAsyncEnumerable();

            return (results, total);
        }

        public Category? GetByTitle(string title, CancellationToken cancellationToken = default)
        {
            return _dbSet.FirstOrDefault(c => c.Title == CategoryName.BuildCategoryName(title).Value);
        }
    }
}
