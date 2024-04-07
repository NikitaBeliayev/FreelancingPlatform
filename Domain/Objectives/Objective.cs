using Domain.Statuses;
using Domain.Categories;
using Domain.Payments;
using Domain.Types;
using Domain.Users;
using Domain.Users.UserDetails;
using Shared;

namespace Domain.Objectives;

public class Objective : Entity
{
	private DateTime _eta;
	public ObjectiveTitle Title { get; set; }
	public ObjectiveDescription Description { get; set; }
	public Payment Payment { get; set; }
	public Guid PaymentId { get; set; }
	public decimal PaymentAmount { get; set; }
	public ObjectiveStatus ObjectiveStatus { get; set; }
	public Guid ObjectiveStatusId { get; set; }
	public ICollection<Category> Categories { get; } = new List<Category>();
	public ObjectiveType Type { get; set; }
	public Guid TypeId { get; set; }
	public string CreatorPublicContacts { get; set; }
    public User Creator { get; set; }
    public Guid CreatorId { get; set; }
    public ICollection<User> Implementors { get; } = new List<User>();
	public byte[]? Attachments { get; set; }
	
	/// <summary>
	/// The estimated time of arrival
	/// </summary>
	public DateTime Eta //change this to Wrapper
	{
		get => _eta;
		set => _eta = value < DateTime.Now.AddDays(1) ? throw new ArgumentException("ETA must be at least +1 day from now") : value;
	}
	
	public Objective(Guid id) : base(id)
	{

	}
	public Objective(Guid id, ObjectiveTitle title, ObjectiveDescription description, Payment payment, decimal paymentAmount, 
		ObjectiveStatus objectiveStatus, ICollection<Category> categories, ObjectiveType type, byte[] attachments, Guid paymentId,
		Guid objectiveStatusId, Guid creatorId, User creator, string creatorPublicContacts, Guid typeId, DateTime eta) : base(id)
	{
		Id = id;
		Title = title;
		Description = description;
		Payment = payment;
		PaymentAmount = paymentAmount;
		ObjectiveStatus = objectiveStatus;
		Categories = categories;
		Type = type;
		Attachments = attachments;
		PaymentId = paymentId;
		ObjectiveStatusId = objectiveStatusId;
		CreatorId = creatorId;
		Creator = creator;
		CreatorPublicContacts = creatorPublicContacts;
		TypeId = typeId;
		Eta = eta;
	}
}
