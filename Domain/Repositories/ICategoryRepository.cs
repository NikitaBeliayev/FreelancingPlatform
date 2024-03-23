using Domain.Categories;

namespace Domain.Repositories

{
    public interface ICategoryRepository
    {
        Task<Category?> CreateByTitleAsync(Category entity, CancellationToken cancellationToken = default);
        Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        IEnumerable<Category> GetByTitleWithPagination(string search, int take, int pageSize, CancellationToken cancellationToken = default);
    }
}
