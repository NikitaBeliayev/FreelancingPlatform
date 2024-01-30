﻿namespace Domain.Objectives.ObjectiveTypes;

public class ObjectiveType
{
	private int _duration;
	private DateTime _eta;

	public ObjectiveType()
	{
	}

	public ObjectiveType(int id, ICollection<Objective>? tasks, ObjectiveTypeTitle typeTitle, DateTime eta, int duration)
	{
		Id = id;
		Tasks = tasks;
		TypeTitle = typeTitle;
		ETA = eta;
		Duration = duration;
	}

	public int Id { get; set; }

	public ICollection<Objective>? Tasks { get; set; }

	public ObjectiveTypeTitle TypeTitle { get; set; }

	/// <summary>
	/// The estimated time of arrival
	/// </summary>
	public DateTime ETA
	{
		get => _eta;
		set => _eta = value < DateTime.Now.AddDays(1) ? throw new ArgumentException("ETA must be at least +1 day from now") : value;
	}

	/// <summary>
	/// Possible working time in hours
	/// </summary>
	public int Duration
	{
		get => _duration;
		set => _duration = value < 8 ? throw new ArgumentException("Duration must be more or equal to 8 hours") : value;
	}
}