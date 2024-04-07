using System.Collections.Immutable;
using Shared;

namespace Domain.Payments;

public static class PaymentVariations
{
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _paymentCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
        new KeyValuePair<Guid, string>( new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"), "Coin")
    );
    
    public static Guid Coin => _paymentCollection.FirstOrDefault(pair => pair.Value == "Coin")!.Key;

    public static Result<string> GetValue(Guid key)
    {
        var possibleElement = _paymentCollection.FirstOrDefault(element => element.Key == key);

        return Result<string>.Success(possibleElement.Value);
    }
}