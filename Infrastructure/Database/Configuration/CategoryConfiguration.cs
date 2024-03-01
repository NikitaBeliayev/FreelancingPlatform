﻿using Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    }
}