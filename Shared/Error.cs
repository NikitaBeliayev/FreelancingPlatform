using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record Error(string Code, string Message, int StatusCode = 500)
    {
        public static readonly Error None = new(string.Empty, string.Empty, 0);
        public Result ToResult() => Result.Failure(this);
    }
}