using Application.Abstraction.Messaging;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Create
{
    public record CreateUserCommand(UserDTO User): ICommand<UserDTO>
    {
    }
}
