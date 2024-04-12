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

        public IAsyncEnumerable<Category> GetByTitleWithPagination(Func<Category, bool> expression, int pageSize, int skip, CancellationToken cancellationToken = default)
        {
            return _dbSet.OrderBy(x => x.Objectives.Count).Skip(skip)
                .Take(pageSize).AsAsyncEnumerable();
        }

        public Category? GetByTitle(string title, CancellationToken cancellationToken = default)
        {
            return _dbSet.FirstOrDefault(c => c.Title == CategoryName.BuildCategoryName(title).Value);
        }
    }
}
