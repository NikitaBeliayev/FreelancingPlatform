using Domain.Objectives.ObjectiveTypes;

namespace Application.Objectives.ObjectiveTypes;
public class ObjectiveTypeDto
{
	public int Id { get; set; }
	public string TypeTitle { get; set; }
	public DateTime ETA { get; set; }
	public int Duration { get; set; }
}
