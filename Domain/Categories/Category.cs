using Domain.Objectives;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Categories;

public class Category : Entity
{
    public CategoryName Title { get; set; }

    public ICollection<Objective> Objectives = new List<Objective>();

    public Category(Guid id) : base(id)
    {
    }

    public Category(Guid id, CategoryName title, List<Objective> objectives) : base(id)
    {
        Title = title;
        Objectives = objectives;
    }
}