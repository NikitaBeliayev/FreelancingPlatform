using Shared;

namespace Domain.Roles.Errors;

public static class RoleNameErrors
{
    public static Error InvalidName =>
        new("Roles.RoleName.InvalidName", $"The Name value must be 2 or 3", 422);
    public static Error AdminNameError =>
        new("Roles.RoleName.AdminNameError", $"An admin cannot be created manually", 422);
}