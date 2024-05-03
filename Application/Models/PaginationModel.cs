
namespace Application.Models
{
    public class PaginationModel<T>
    {
        public int Total { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public IEnumerable<T>? Results { get; set; }

        public PaginationModel()
        {

        }

        public PaginationModel(int total, IEnumerable<T> results, int page, int pageSize)
        {
            Total = total;
            Results = results;
            Next = total > pageSize * page ? $"?pageNum={page + 1}&pageSize={pageSize}" : null;
            Previous = page > 1 ? $"?pageNum={page - 1}&pageSize={pageSize}" : null;
        }
    }
}
