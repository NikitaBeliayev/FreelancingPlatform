using Domain.Objectives;
using Shared;

namespace Domain.Payments;

public class Payment : Entity
{
    public PaymentName Name { get; set; }
    public ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public Payment(Guid id) : base(id)
    { }

    public Payment(Guid id, PaymentName paymentType, ICollection<Objective> objectives) : base(id)
    {
        Name = paymentType;
        Objectives = objectives;
    }
}