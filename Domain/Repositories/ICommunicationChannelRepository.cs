using Domain.CommunicationChannels;

namespace Domain.Repositories;

public interface ICommunicationChannelRepository : IRepository<CommunicationChannel>
{
    void ChangeStateToUnchanged(CommunicationChannel communicationChannel);
}