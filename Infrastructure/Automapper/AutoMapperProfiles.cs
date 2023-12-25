using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Users;
using AutoMapper;
using Domain.Users;

namespace Infrastructure.Automapper
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<Name, string>().ConvertUsing(new NameConverter());
                CreateMap<User, UserDTO>().ReverseMap();

            }
        }
    }
}
