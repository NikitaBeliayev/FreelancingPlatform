using Domain.Objectives;
using Shared;

namespace Domain.Types;

public class ObjectiveType : Entity
{
	public string Description { get; set; } = string.Empty;

	public ObjectiveType(Guid id) : base(id)
	{
	}

	public ObjectiveType(Guid id, ICollection<Objective> tasks, ObjectiveTypeTitle typeTitle, ObjectiveTypeDuration duration, string description) : base(id)
	{
		Objectives = tasks;
		TypeTitle = typeTitle;
		Duration = duration;
		Description = description;
	}


	public ICollection<Objective> Objectives { get; } = new List<Objective>();

	public ObjectiveTypeTitle TypeTitle { get; set; }
	

	/// <summary>
	/// Possible working time in hours
	/// </summary>
	public ObjectiveTypeDuration Duration { get; set; }
}