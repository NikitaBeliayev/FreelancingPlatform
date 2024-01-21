namespace Domain.CommunicationChannels.Repositories;

public interface ICommunicationChannelRepository
{
    Task<CommunicationChannel> GetCommunicationChannelByIdAsync(int id, CancellationToken cancellationToken);
}