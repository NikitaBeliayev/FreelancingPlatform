using AutoMapper;
using Domain.Users.UserDetails;

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
