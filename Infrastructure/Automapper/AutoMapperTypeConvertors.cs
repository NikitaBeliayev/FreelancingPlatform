using AutoMapper;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Automapper
{
    public class NameConverter : ITypeConverter<Name, string>
    {
        public string Convert(Name source, string destination, ResolutionContext context)
        {
            return source.Value;
        }
    }
}
