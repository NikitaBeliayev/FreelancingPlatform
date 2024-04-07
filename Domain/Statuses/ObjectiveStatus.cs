using Domain.Objectives;
using Shared;

namespace Domain.Statuses
{
    public class ObjectiveStatus : Entity
    {
        public ObjectiveStatusTitle Title { get; set; }
        public ICollection<Objective> Objectives { get; } = new List<Objective>();


        public ObjectiveStatus(Guid id) : base(id)
        {
        }

        public ObjectiveStatus(Guid id, ObjectiveStatusTitle title, ICollection<Objective> objectivesCollections) : base(id)
        {
            Title = title;
            Objectives = objectivesCollections;
        }
    }
}
