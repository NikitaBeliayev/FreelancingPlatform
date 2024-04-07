using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Infrastructure.Database.Configuration;

public class UserCommunicationChannelConfiguration : IEntityTypeConfiguration<UserCommunicationChannel>
{
    public void Configure(EntityTypeBuilder<UserCommunicationChannel> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.LastEmailSentAt).IsRequired();
        builder.HasData(new List<UserCommunicationChannel>()
        {
            new UserCommunicationChannel(new Guid("03251b18-6de8-4682-a0bd-ce44aecd8e83"), null, new Guid("d3b8c19b-5d8b-492c-914a-b1b0fa5370ca"),
                false, new Guid("9f07d2ac-6009-405d-b329-c517bcc5ef67"), null, new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.UtcNow),
            new UserCommunicationChannel(new Guid("7f478c61-b38d-450d-b02b-eacb49797ecf"), null, new Guid("88755139-42b8-415b-84df-04c639d9b47a"), 
                false, new Guid("6f189094-f7e2-4e40-8d8b-c45054be7b96"), null, new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.UtcNow),
            new UserCommunicationChannel(new Guid("021e1ca4-3c76-4460-bcb5-ec397ac3a5ea"), null, new Guid("3dc1e776-0e75-4577-ab99-ed85ba86fec5"), 
                false, new Guid("c24652bd-00bd-48b7-b5e2-59f0094f1e2e"), null, new Guid("dae98ace-7c5b-c3d6-df29-29543a9af92d"), DateTime.UtcNow)
        });
    }
}   