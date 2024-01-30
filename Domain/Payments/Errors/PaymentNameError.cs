using Shared;

namespace Domain.Payments.Errors;

public class PaymentNameErrors
{
    public static Error InvalidName =>
        new("Roles.RoleName.InvalidName", $"The Name value must be 1", 422);
}