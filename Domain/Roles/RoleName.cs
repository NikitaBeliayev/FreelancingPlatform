using Domain.Roles.Errors;
using Shared;

namespace Domain.Roles;

public sealed class RoleName
{
    public string Value { get;}

    private RoleName(string roleName)
    {
        Value = roleName;
    }
    
    public static Result<RoleName> BuildRoleName(Guid value)
    {
        return value == RoleNameVariations.Admin || (value != RoleNameVariations.Customer && value != RoleNameVariations.Implementer) ? 
            Result<RoleName>.Failure(null, new Error("", "", 500)) 
            : Result<RoleName>.Success(new RoleName(RoleNameVariations.GetValue(value).Value!));
    }
    
    /// <summary>
    /// Use this method only for ef core configuration
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public static Result<RoleName> BuildRoleNameWithoutValidation(string roleName)
    {
        return Result<RoleName>.Success(new RoleName(roleName));
    }
}