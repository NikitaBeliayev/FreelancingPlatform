using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) => 
            new("Users.NotFound", $"The user with id {id} has not been found");
    }
}
