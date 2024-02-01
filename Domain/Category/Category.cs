using Domain.Objectives;

namespace Domain.Category;

public class Category
{
    public int Id { get; set; }
    public CategoryName Title { get; set; }

    public ICollection<Objective> ObjectiveCollection = new List<Objective>();
}