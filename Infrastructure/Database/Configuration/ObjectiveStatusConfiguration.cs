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
            builder.HasKey(e => e.Id);
            ConstructorInfo? privateConstructor = typeof(ObjectiveStatusTitle).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, [typeof(ObjectiveStatusTitleType)]);

            if (privateConstructor is null)
            {
                throw new NullReferenceException("Constructor not found");
            }

            builder.HasData(new List<ObjectiveStatus>()
            {
            new ObjectiveStatus(1, (ObjectiveStatusTitle)privateConstructor.Invoke([ObjectiveStatusTitleType.Draft]), new List<Objective>()),
            new ObjectiveStatus(2, (ObjectiveStatusTitle)privateConstructor.Invoke([ObjectiveStatusTitleType.InProgress]), new List<Objective>()),
            new ObjectiveStatus(3, (ObjectiveStatusTitle)privateConstructor.Invoke([ObjectiveStatusTitleType.WaitingForAssignment]), new List<Objective>()),
            new ObjectiveStatus(4, (ObjectiveStatusTitle)privateConstructor.Invoke([ObjectiveStatusTitleType.WaitingForApproval]), new List<Objective>()),
            new ObjectiveStatus(5, (ObjectiveStatusTitle)privateConstructor.Invoke([ObjectiveStatusTitleType.Done]), new List<Objective>())
            });
            builder.Property(e => e.Title)
                .HasConversion(value => value.Value,
                    value => ObjectiveStatusTitle.BuildStatusTitle(
                        (int)(ObjectiveStatusTitleType)Enum.Parse(typeof(ObjectiveStatusTitleType), value)).Value!);
        }
    }
}
