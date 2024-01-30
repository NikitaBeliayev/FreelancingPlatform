namespace Domain.CommunicationChannels.Repositories;

public interface ICommunicationChannelRepository
{
    void ChangeStateToUnchanged(CommunicationChannel communicationChannel);
}