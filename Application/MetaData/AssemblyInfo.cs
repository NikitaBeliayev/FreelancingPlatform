using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.MetaData
{
    public static class AssemblyInfo
    {
        public static Assembly Assembly => typeof(AssemblyInfo).Assembly;
    }
}
