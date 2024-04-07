using Domain.Payments.Errors;
using Shared;

namespace Domain.Payments;

public class PaymentName
{
    public string Value { get; }

    private PaymentName(string paymentName)
    {
        Value = paymentName;
    }

    public static Result<PaymentName> BuildName(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? Result<PaymentName>.Failure(null, new Error("", "", 500)) 
            : Result<PaymentName>.Success(new PaymentName(value));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    public static Result<PaymentName> BuildNameWithoutValidation(string payment)
    {
        return Result<PaymentName>.Success(new PaymentName(payment));
    }
}