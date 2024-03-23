using Domain.Objectives;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Categories;

public class Category
{
    [Key]
    public int? Id { get; set; }
    public CategoryName Title { get; set; }

    public ICollection<Objective> Objectives = new List<Objective>();

    public Category() { }
    public Category(CategoryName title, List<Objective> objectives)
    {
        Title = title;
        Objectives = objectives;
    }
    public Category(int id, CategoryName title, List<Objective> objectives)
    {
        Id = id;
        Title = title;
        Objectives = objectives;
    }
}