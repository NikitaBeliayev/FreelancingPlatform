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
        
        builder.HasData(new CommunicationChannel(new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), 
            CommunicationChannelName.BuildCommunicationChannelNameWithoutValidation("Email").Value!, 
            new List<UserCommunicationChannel>()));
            
        builder.HasMany(e => e.UserCommunicationChannels)
            .WithOne(e => e.CommunicationChannel)
            .HasForeignKey(e => e.CommunicationChannelId)
            .IsRequired();
        
        builder.Property(x => x.Name).HasConversion(
            value => value.Value,
            value => CommunicationChannelName.BuildCommunicationChannelNameWithoutValidation(value).Value!);
    }
}