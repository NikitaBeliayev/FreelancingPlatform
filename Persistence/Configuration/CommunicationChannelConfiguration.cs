using Domain.CommunicationChannels;
using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Persistence.Configuration;

public class CommunicationChannelConfiguration : IEntityTypeConfiguration<CommunicationChannel>
{
    public void Configure(EntityTypeBuilder<CommunicationChannel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new CommunicationChannel(1, CommunicationChannelType.Email,
            new List<UserCommunicationChannel>()));
            
        builder.HasMany(e => e.UserCommunicationChannels)
            .WithOne(e => e.CommunicationChannel)
            .HasForeignKey(e => e.CommunicationChannelId)
            .IsRequired();
        
        builder.Property(x => x.Type).HasConversion(
            value => Enum.GetName(value.GetType(), value),
            value => (CommunicationChannelType)Enum.Parse(typeof(CommunicationChannelType), value));
    }
}