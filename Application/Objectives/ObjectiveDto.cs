using Application.Objectives.Categories;
using Application.Objectives.ObjectiveStatus;
using Application.Objectives.ObjectiveTypes;
using Application.Payments;
using Application.Users;

namespace Application.Objectives;

public class ObjectiveDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PaymentDto Payment { get; set; }
    public decimal PaymentAmount { get; set; }
    public ObjectiveStatusDto ObjectiveStatus { get; set; }
    public ICollection<CategoryDto> Categories { get; } = new List<CategoryDto>();
    public ObjectiveTypeDto Type { get; set; }
    public byte[]? Attachments { get; set; }
    public string CreatorPublicContacts { get; set; }
    public UserDto Creator { get; set; }
}