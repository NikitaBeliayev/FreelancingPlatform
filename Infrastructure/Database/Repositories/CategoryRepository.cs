using Domain.Categories;
using Domain.Repositories;

namespace Infrastructure.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> CreateByTitleAsync(Category entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Category.AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Category.FindAsync(new object[] { id }, cancellationToken);
        }

        public IEnumerable<Category> GetByTitleWithPagination(string search, int pageSize, int skip, CancellationToken cancellationToken = default)
        {
            var categories = _dbContext.Category.ToList();

            return categories
                .Where(x => x.Title.Value.ToLower() == search.ToLower())
                .Skip(skip)
                .Take(pageSize)
                .AsEnumerable();
        }
    }
}
