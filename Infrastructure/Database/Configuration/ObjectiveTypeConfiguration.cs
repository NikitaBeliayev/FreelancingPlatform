﻿using System.Reflection;
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
            ConstructorInfo? privateConstructor = typeof(ObjectiveTypeTitle).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, [typeof(string)]);

            if (privateConstructor is null)
            {
                throw new NullReferenceException("Constructor not found");
            }

            builder.HasData(new List<ObjectiveType>()
            {
                new ObjectiveType(new Guid("2247d42d-a645-bc96-0b4b-944db2a8b519"), 
                    new List<Objective>(), (ObjectiveTypeTitle)privateConstructor.Invoke(new object[] { ObjectiveTypeVariations.GetValue(ObjectiveTypeVariations.Individual).Value! }), 8),
                new ObjectiveType(new Guid("a28f84ac-f428-a29b-b8a5-fbd76596817d"),
                    new List<Objective>(), (ObjectiveTypeTitle)privateConstructor.Invoke(new object[] { ObjectiveTypeVariations.GetValue(ObjectiveTypeVariations.Team).Value! }), 8)
            });

            builder.Property(e => e.TypeTitle)
                .HasConversion(value => value.Title,
                    value => ObjectiveTypeTitle.BuildObjectiveTypeTitleWithoutValidation(value).Value!);
        }
    }
}