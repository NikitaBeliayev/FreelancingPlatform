namespace Domain.Payments;

public class Payment
{
    public int Id { get; set; }
    
    public PaymentName Name { get; set; }
    public ICollection<string> Objectives { get; set; } = new List<string>();

    public Payment()
    {
        
    }

    public Payment(int id, PaymentName paymentType, ICollection<string> objectives)
    {
        Id = id; 
        Name = paymentType;
        Objectives = objectives;
    }
}