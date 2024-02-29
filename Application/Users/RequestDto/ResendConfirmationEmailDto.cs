using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.RequestDto
{
    public class ResendConfirmationEmailDto
    {
        public Guid userId { get; set; }
    }
}
