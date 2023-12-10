using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record Error(string Code, string msg)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public Result ToResult() => Result.Failure(this);
    }
}
