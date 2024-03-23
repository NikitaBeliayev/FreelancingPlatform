using Application.Objectives.Categories;
using Application.Payments;

namespace Application.Objectives.RequestDto;

public class ObjectiveUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public PaymentDto Payment { get; set; }
    public decimal PaymentAmount { get; set; }
    public byte[]? Attachments { get; set; }
    public ICollection<CategoryDto> Categories { get; } = new List<CategoryDto>();
}