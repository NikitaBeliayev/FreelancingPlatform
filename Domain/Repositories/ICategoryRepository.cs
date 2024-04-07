using Domain.Categories;

namespace Domain.Repositories

{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetByTitleWithPagination(string search, int take, int pageSize, CancellationToken cancellationToken = default);
    }
}
