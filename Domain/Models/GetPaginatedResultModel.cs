using Shared;

namespace Domain.Models
{
    public class GetPaginatedResultModel<TEntity> where TEntity : Entity
    {
        public List<TEntity> result;
        public int count;
    }
}
