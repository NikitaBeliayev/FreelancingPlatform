using Domain.Objectives;

namespace Domain.Statuses
{
    public class ObjectiveStatus
    {
        public int Id { get; set; }
        public ObjectiveStatusTitle Title { get; set; }
        public ICollection<Objective> Objectives { get; } = new List<Objective>();
    

        public ObjectiveStatus()
        {
        }

        public ObjectiveStatus(int id, ObjectiveStatusTitle title, ICollection<Objective> objectivesCollections)
        {
            Id = id;
            Title = title;
            Objectives = objectivesCollections;
        }
    }
}
