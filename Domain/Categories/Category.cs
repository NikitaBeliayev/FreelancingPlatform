using Domain.Objectives;

namespace Domain.Categories;

public class Category
{
    public int Id { get; set; }
    public CategoryName Title { get; set; }

    public ICollection<Objective> Objectives = new List<Objective>();
}