using System.Reflection;
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
        
        builder.HasData(new List<Category>()
            {
                new Category(new Guid("9fa1efd7-7dbe-7239-fd5a-db6024223d74"), CategoryName.BuildCategoryNameWithoutValidation("C#").Value!, new List<Objective>()),
                new Category(new Guid("888723d5-1e0e-28a2-17a7-2d3759213819"), CategoryName.BuildCategoryNameWithoutValidation("Java").Value!, new List<Objective>()),
                new Category(new Guid("8c4f32f0-3202-4af4-9532-a89219192219"), CategoryName.BuildCategoryNameWithoutValidation("JavaScript").Value!, new List<Objective>()),
                new Category(new Guid("a65af977-4c65-4665-814c-0106f46059ed"), CategoryName.BuildCategoryNameWithoutValidation("UI/UX Design").Value!, new List<Objective>()),
                new Category(new Guid("153f181b-8351-4e76-9369-70c5992dc688"), CategoryName.BuildCategoryNameWithoutValidation("ReactJS").Value!, new List<Objective>()),
                new Category(new Guid("0310ad4d-2719-4418-a90c-f46ffff81cad"), CategoryName.BuildCategoryNameWithoutValidation("Angular").Value!, new List<Objective>()),
                new Category(new Guid("602ceb55-7748-48e3-9c4f-b92ccbbd04b2"), CategoryName.BuildCategoryNameWithoutValidation(".Net").Value!, new List<Objective>()),
                new Category(new Guid("dbb705b5-20f4-4588-b573-2732c72cb5c7"), CategoryName.BuildCategoryNameWithoutValidation("Copywriting").Value!, new List<Objective>()),
                new Category(new Guid("03622698-9525-489a-bb52-8eb02f7ee537"), CategoryName.BuildCategoryNameWithoutValidation("Manual quality assurance").Value!, new List<Objective>()),
                new Category(new Guid("6b4bea40-57ea-41fc-8754-0197d8a52e9a"), CategoryName.BuildCategoryNameWithoutValidation("Automation quality assurance").Value!, new List<Objective>()),
                new Category(new Guid("20d7d92b-2380-4c0c-9980-b1d995af76db"), CategoryName.BuildCategoryNameWithoutValidation("Document management").Value!, new List<Objective>()),
                new Category(new Guid("39fd83eb-97a4-4add-a921-d9fdf22e6c40"), CategoryName.BuildCategoryNameWithoutValidation("Content management").Value!, new List<Objective>()),
                new Category(new Guid("f83c20ff-ec35-4258-97b7-400e18a64e9a"), CategoryName.BuildCategoryNameWithoutValidation("Languages").Value!, new List<Objective>()),
                new Category(new Guid("a9fd5bad-78ee-4b4e-b274-33dc81bdab06"), CategoryName.BuildCategoryNameWithoutValidation("Artificial intelligence").Value!, new List<Objective>()),
                new Category(new Guid("6d0d2336-d191-4e40-abf8-27d081c464ce"), CategoryName.BuildCategoryNameWithoutValidation("Creativity").Value!, new List<Objective>()),
                new Category(new Guid("afc6d9d0-7c97-4f99-abf0-22a56ab4f454"), CategoryName.BuildCategoryNameWithoutValidation("School specialist’s assistance").Value!, new List<Objective>()),
                new Category(new Guid("c8f290a1-ce07-4429-b0c4-90f6682c0941"), CategoryName.BuildCategoryNameWithoutValidation("Other").Value!, new List<Objective>())
            });
    }
}