using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public UserDto()
        {           
        }
        public UserDto(Guid? id, string firstName, string secondName)
        {
            Id = id;
            FirstName = firstName;
            LastName = secondName;
        }
    }
}
