using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class User : Entity
    {
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public User(Guid id, Name firstName, Name lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
