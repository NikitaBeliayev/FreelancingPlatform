using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Objectives.ObjectiveTypes;
using Domain.Roles;
using Domain.Objectives;
using Domain.Types;

namespace Infrastructure.Database.Configuration
{
	public class ObjectiveTypeConfiguration : IEntityTypeConfiguration<ObjectiveType>
	{
		public void Configure(EntityTypeBuilder<ObjectiveType> builder)
		{
			builder.HasKey(e => e.Id);
			ConstructorInfo? privateConstructor = typeof(ObjectiveTypeTitle).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, [typeof(ObjectiveTypeVariations)]);

			if (privateConstructor is null)
			{
				throw new NullReferenceException("Constructor not found");
			}

			builder.HasData(new List<ObjectiveType>()
			{
				new ObjectiveType(1, new List<Objective>(), (ObjectiveTypeTitle)privateConstructor.Invoke(new object[] { ObjectiveTypeVariations.Individual }), DateTime.Now.AddDays(2), 8),
				new ObjectiveType(2, new List<Objective>(), (ObjectiveTypeTitle)privateConstructor.Invoke(new object[] { ObjectiveTypeVariations.Group }), DateTime.Now.AddDays(2), 8),
				new ObjectiveType(3, new List<Objective>(), (ObjectiveTypeTitle)privateConstructor.Invoke(new object[] { ObjectiveTypeVariations.Team }), DateTime.Now.AddDays(2), 8)
			});

			builder.Property(e => e.TypeTitle)
				.HasConversion(value => value.Title,
					value => ObjectiveTypeTitle.BuildObjectiveTypeTitleWithoutValidation(
						(int)(ObjectiveTypeVariations)Enum.Parse(typeof(ObjectiveTypeVariations), value)).Value!);
		}
	}
}
