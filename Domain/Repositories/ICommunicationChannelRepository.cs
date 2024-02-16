using Domain.CommunicationChannels;

namespace Domain.Repositories;

public interface ICommunicationChannelRepository
{
    void ChangeStateToUnchanged(CommunicationChannel communicationChannel);
}