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
        public EmailAddress Email { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Password Password { get; set; }

        public User(Guid id, EmailAddress email, Name firstName, Name lastName, Password password) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}
