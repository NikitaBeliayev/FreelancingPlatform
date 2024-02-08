using Domain.Payments.Errors;
using Shared;

namespace Domain.Payments;

public class PaymentName
{
    public string Value { get; }

    private PaymentName(PaymentType paymentType)
    {
        Value = paymentType.ToString();
    }

    public static Result<PaymentName> BuildName(int payment)
    {
        if (payment is not 1)
        {
            return  Result<PaymentName>.Failure(null, PaymentNameErrors.InvalidName);
        }
        return Result<PaymentName>.Success(new PaymentName(((PaymentType)payment)));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    public static Result<PaymentName> BuildNameWithoutValidation(int payment)
    {
        return Result<PaymentName>.Success(new PaymentName(((PaymentType)payment)));
    }
}