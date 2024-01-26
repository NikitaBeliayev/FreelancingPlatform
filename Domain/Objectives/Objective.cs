
namespace Domain.Objectives
{
    public class Objective // placeholder class needed only to build project and pass pipeline, replace in merge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Objective()
        {

        }
        public Objective(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
