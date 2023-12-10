using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public UserDTO()
        {           
        }
        public UserDTO(Guid? id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            LastName = secondName;
        }
    }
}
