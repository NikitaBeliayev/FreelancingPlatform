using Domain.Categories;
using Domain.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
           
        }

        public IEnumerable<Category> GetByTitleWithPagination(string search, int pageSize, int skip, CancellationToken cancellationToken = default)
        {
            var categories = _dbSet.ToList();

            return categories
                .Where(x => x.Title.Value.ToLower() == search.ToLower())
                .Skip(skip)
                .Take(pageSize);
        }
    }
}
