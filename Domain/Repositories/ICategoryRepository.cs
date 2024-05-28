using Domain.Categories;
using Shared;
using System.Linq.Expressions;

namespace Domain.Repositories

{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Category? GetByTitle(string title, CancellationToken cancellationToken = default);
    }
}
