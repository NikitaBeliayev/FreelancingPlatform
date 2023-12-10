using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public static class NameErrors
    {
        public static Error NullOrEmpty =>
            new("Users.Name.NullOrEmpty", $"The Name value cannot be NullOrEmpty");
    }
}
