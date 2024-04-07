using Domain.Objectives;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .HasConversion(value => value.ToString(),
                value => PaymentName.BuildNameWithoutValidation(value!).Value!)
            .IsRequired();

        builder.HasData(new Payment(new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"), 
            PaymentName.BuildNameWithoutValidation("Coin").Value!, new List<Objective>()));
    }
}