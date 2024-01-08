﻿using Domain.UserCommunicationChannels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class UserCommunicationChannelConfiguration : IEntityTypeConfiguration<UserCommunicationChannel>
{
    public void Configure(EntityTypeBuilder<UserCommunicationChannel> builder)
    {
        builder.HasKey(e => e.Id);
    }
}