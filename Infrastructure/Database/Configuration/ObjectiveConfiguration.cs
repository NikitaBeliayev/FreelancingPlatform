using Domain.Objectives;
using Domain.Payments;
using Domain.Statuses;
using Domain.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ObjectiveConfiguration : IEntityTypeConfiguration<Objective>
{
    public void Configure(EntityTypeBuilder<Objective> builder)
    {
        builder.HasKey(e => e.Id);
        
        
        builder.Property(e => e.Title)
            .HasConversion(value => value.Value,
                value => ObjectiveTitle.BuildNameWithoutValidation(value).Value!);
        builder.Property(e => e.Description)
            .HasConversion(value => value.Value,
                value => ObjectiveDescription.BuildNameWithoutValidation(value).Value!);

        builder.HasMany(e => e.Categories)
            .WithMany(e => e.Objectives);
        
        builder.HasOne(e => e.Payment)
            .WithMany(e => e.Objectives)
            .HasForeignKey(e => e.PaymentId)
            .IsRequired();
        
        builder.HasOne(e => e.ObjectiveStatus)
            .WithMany(e => e.Objectives)
            .HasForeignKey(e => e.ObjectiveStatusId)
            .IsRequired();

        builder.HasOne(e => e.Type)
            .WithMany(e => e.Objectives)
            .HasForeignKey(e => e.TypeId)
            .IsRequired();
        
        builder.HasMany(e => e.Users)
            .WithMany(e => e.Objectives);

    }
}