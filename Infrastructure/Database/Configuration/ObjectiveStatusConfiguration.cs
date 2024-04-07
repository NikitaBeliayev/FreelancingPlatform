using Domain.Objectives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;
using Domain.Statuses;

namespace Infrastructure.Database.Configuration
{
    public class ObjectiveStatusConfiguration : IEntityTypeConfiguration<ObjectiveStatus>
    {
        public void Configure(EntityTypeBuilder<ObjectiveStatus> builder)
        {
            builder.HasData(new List<ObjectiveStatus>()
            {
                new (new Guid("6cb13af0-83d5-c772-7ba4-5a3d9a5a1cb9"), 
                    ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(ObjectiveStatusTitleVariations.GetValue(ObjectiveStatusTitleVariations.Draft).Value!).Value!,
                    new List<Objective>()),
                new (new Guid("c9b0e0b6-fb0c-fedd-767f-137f8066d1df"),
                    ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(ObjectiveStatusTitleVariations.GetValue(ObjectiveStatusTitleVariations.InProgress).Value!).Value!, 
                    new List<Objective>()),
                new (new Guid("327db9d4-0282-c319-b047-dcf22483e225"), 
                    ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(ObjectiveStatusTitleVariations.GetValue(ObjectiveStatusTitleVariations.WaitingForAssignment).Value!).Value!, 
                    new List<Objective>()),
                new (new Guid("2f2f54aa-46dd-29d0-6459-2afdb5e950ee"), 
                    ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(ObjectiveStatusTitleVariations.GetValue(ObjectiveStatusTitleVariations.WaitingForApproval).Value!).Value!, 
                    new List<Objective>()),
                new (new Guid("e26529f9-a7c8-b3af-c1b9-a5c09a263636"), 
                ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(ObjectiveStatusTitleVariations.GetValue(ObjectiveStatusTitleVariations.Done).Value!).Value!, 
                new List<Objective>())
            });
            builder.Property(e => e.Title)
                .HasConversion(value => value.Value,
                    value => ObjectiveStatusTitle.BuildStatusTitleWithoutValidation(value).Value!);
        }
    }
}
