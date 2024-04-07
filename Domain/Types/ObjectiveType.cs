using Domain.Objectives;
using Shared;

namespace Domain.Types;

public class ObjectiveType : Entity
{
	private int _duration;
	private DateTime _eta;

	public ObjectiveType(Guid id) : base(id)
	{
	}

	public ObjectiveType(Guid id, ICollection<Objective> tasks, ObjectiveTypeTitle typeTitle,int duration) : base(id)
	{
		Objectives = tasks;
		TypeTitle = typeTitle;
		Duration = duration;
	}


	public ICollection<Objective> Objectives { get; } = new List<Objective>();

	public ObjectiveTypeTitle TypeTitle { get; set; }
	

	/// <summary>
	/// Possible working time in hours
	/// </summary>
	public int Duration //change this to Wrapper
	{
		get => _duration;
		set => _duration = value < 8 ? throw new ArgumentException("Duration must be more or equal to 8 hours") : value;
	}
}