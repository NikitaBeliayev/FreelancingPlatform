using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid id, string email);
        string GenerateRefreshToken(Guid id, string email);
    }
}
