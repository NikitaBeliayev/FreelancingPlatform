using Domain.Roles.Errors;
using Shared;

namespace Domain.Roles;

public sealed record class RoleName
{
    public string Value { get;}

    private RoleName(RoleNameType roleType)
    {
        Value = roleType.ToString();
    }
    
    public static Result<RoleName> BuildRoleName(int role)
    {
        if (role is 1)
        {
            return Result<RoleName>.Failure(null, RoleNameErrors.AdminNameError);
        }
        if (role is not 2 && role is not 3)
        {
            return Result<RoleName>.Failure(null, RoleNameErrors.InvalidName);
        }

        return Result<RoleName>.Success(new RoleName(((RoleNameType)role)));
    }
}