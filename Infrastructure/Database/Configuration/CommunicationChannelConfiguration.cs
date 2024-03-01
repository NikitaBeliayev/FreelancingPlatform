using System.Reflection;
using Domain.CommunicationChannels;
using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Database.Configuration;

public class CommunicationChannelConfiguration : IEntityTypeConfiguration<CommunicationChannel>
{
    public void Configure(EntityTypeBuilder<CommunicationChannel> builder)
    {
        builder.HasKey(x => x.Id);

        ConstructorInfo? privateConstructor = typeof(CommunicationChannelName).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, [typeof(CommunicationChannelNameType)]);
        
        if (privateConstructor is null)
        {
            throw new NullReferenceException("Constructor not found");
        }
        
        builder.HasData(new CommunicationChannel(1, (CommunicationChannelName)privateConstructor.Invoke([CommunicationChannelNameType.Email]),
            new List<UserCommunicationChannel>()));
            
        builder.HasMany(e => e.UserCommunicationChannels)
            .WithOne(e => e.CommunicationChannel)
            .HasForeignKey(e => e.CommunicationChannelId)
            .IsRequired();
        
        builder.Property(x => x.Name).HasConversion(
            value => value.Value,
            value => CommunicationChannelName.BuildCommunicationChannelNameWithoutValidation(
                (int)(CommunicationChannelNameType)Enum.Parse(typeof(CommunicationChannelNameType), value)).Value!);
    }
}