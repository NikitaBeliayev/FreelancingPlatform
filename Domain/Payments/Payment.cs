using Domain.Objectives;

namespace Domain.Payments;

public class Payment
{
    public int Id { get; set; }
    
    public PaymentName Name { get; set; }
    public ICollection<Objective> Objectives { get; set; } = new List<Objective>();

    public Payment()
    {
        
    }

    public Payment(int id, PaymentName paymentType, ICollection<Objective> objectives)
    {
        Id = id; 
        Name = paymentType;
        Objectives = objectives;
    }
}