using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Objectives.ObjectiveStatus
{
    public class ObjectiveStatus
    {
        public int Id { get; set; }
        public ObjectiveStatusTitle Title { get; set; }
        public ICollection<Objective> Objectives = new List<Objective>(); // replase string with objective after implemntaion

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
