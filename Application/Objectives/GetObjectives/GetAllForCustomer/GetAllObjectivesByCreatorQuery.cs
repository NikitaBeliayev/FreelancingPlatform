using Application.Abstraction.Messaging;
using Application.Objectives.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public class GetAllObjectivesByCreatorQuery : IQuery<IEnumerable<ResponseObjectiveDto>>
    {
        public Guid CreatorId { get; set; }

        public GetAllObjectivesByCreatorQuery(Guid creatorId)
        {
            CreatorId = creatorId;
        }
    }
}
