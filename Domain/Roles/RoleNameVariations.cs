using System.Collections.Immutable;
using Shared;

namespace Domain.Roles;

public static class RoleNameVariations
{
    private static readonly ImmutableList<KeyValuePair<Guid, string>> _roleNameCollection = ImmutableList.Create<KeyValuePair<Guid, string>>(
        new KeyValuePair<Guid, string>( new Guid("00edafe3-b047-5980-d0fa-da10f400c1e5"), "Admin"),
        new KeyValuePair<Guid, string>(new Guid("1d6026ce-0dac-13ea-8b72-95f02b7620a7"), "Customer"),
        new KeyValuePair<Guid, string>(new Guid("e438dde5-34d8-f76d-aecb-26a8d882d530"), "Implementer")
    );
    
    public static Guid Admin => _roleNameCollection.FirstOrDefault(pair => pair.Value == "Admin")!.Key;
    public static Guid Customer => _roleNameCollection.FirstOrDefault(pair => pair.Value == "Customer")!.Key;
    public static Guid Implementer => _roleNameCollection.FirstOrDefault(pair => pair.Value == "Implementer")!.Key;

    public static Result<string> GetValue(Guid key)
    {
        var possibleElement = _roleNameCollection.FirstOrDefault(element => element.Key == key);

        return Result<string>.Success(possibleElement.Value);
    }
}