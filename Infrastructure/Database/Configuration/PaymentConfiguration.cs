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
                value => PaymentName.BuildNameWithoutValidation((int)(PaymentType)Enum.Parse(typeof(PaymentType), value)).Value!)
            .IsRequired();
    }
}