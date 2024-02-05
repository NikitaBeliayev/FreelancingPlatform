using Domain.Statuses;
using Domain.Categories;
using Domain.Payments;
using Domain.Types;
using Domain.Users.UserDetails;

namespace Domain.Objectives;

public class Objective
{
	public Guid Id { get; set; }
	public ObjectiveTitle Title { get; set; }
	public ObjectiveDescription Description { get; set; }
	public Payment Payment { get; set; }
	public int PaymentId { get; set; }
	public decimal PaymentAmount { get; set; }
	public ObjectiveStatus ObjectiveStatus { get; set; }
	public int ObjectiveStatusId { get; set; }
	public ICollection<Category> Categories { get; } = new List<Category>();
	public ObjectiveType Type { get; set; }
	public int TypeId { get; set; }

	public ICollection<User> Users { get; } = new List<User>();
	//public ICollection<string> Contacts { get; } = new List<string>();
	public byte[]? Attachments { get; set; }
	
	public Objective()
	{

	}
	public Objective(Guid id, ObjectiveTitle title, ObjectiveDescription description, Payment payment, decimal paymentAmount, 
		ObjectiveStatus objectiveStatus, ICollection<Category> categories, ObjectiveType type, byte[] attachments, int paymentId,
		int objectiveStatusId)
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
	}
}
