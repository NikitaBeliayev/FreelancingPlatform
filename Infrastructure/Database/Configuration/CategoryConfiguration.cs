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
        builder.HasIndex(e => e.Title)
            .IsUnique();
        builder.Property(e => e.Title)
            .HasConversion(value => value.Value,
                value => CategoryName.BuildCategoryNameWithoutValidation(value).Value!);

        var cSharp = new Category(1, CategoryName.BuildCategoryNameWithoutValidation("C#").Value, new List<Objective>());
        var java = new Category(2, CategoryName.BuildCategoryNameWithoutValidation("Java").Value, new List<Objective>());
        var js = new Category(3, CategoryName.BuildCategoryNameWithoutValidation("JS").Value, new List<Objective>());

        builder.HasData(new List<Category>()
            {
                cSharp,
                java,
                js
            });
    }
}