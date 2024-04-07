using Domain.Categories;
using Domain.Users.UserDetails;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Objectives;

namespace Infrastructure.Database.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Title).IsUnique();
        builder.Property(e => e.Title)
            .HasConversion(value => value.Value,
                value => CategoryName.BuildCategoryNameWithoutValidation(value).Value!);

        var cSharp = new Category(new Guid("9fa1efd7-7dbe-7239-fd5a-db6024223d74"), CategoryName.BuildCategoryNameWithoutValidation("C#").Value!, new List<Objective>());
        var java = new Category(new Guid("888723d5-1e0e-28a2-17a7-2d3759213819"), CategoryName.BuildCategoryNameWithoutValidation("Java").Value!, new List<Objective>());
        var js = new Category(new Guid("8c4f32f0-3202-4af4-9532-a89219192219"), CategoryName.BuildCategoryNameWithoutValidation("JS").Value!, new List<Objective>());

        builder.HasData(new List<Category>()
            {
                cSharp,
                java,
                js
            });
    }
}