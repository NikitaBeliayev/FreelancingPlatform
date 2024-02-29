using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.ResetPasswordDto;

public class ResetPasswordDto
{
    public string Password { get; set; }
    public Guid Token { get; set; }
}
