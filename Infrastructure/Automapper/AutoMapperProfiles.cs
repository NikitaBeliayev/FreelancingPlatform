using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles;
using Application.Users;
using Application.Users.RequestDto;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.Roles;
using Domain.Users;
using Domain.Users.UserDetails;

namespace Infrastructure.Automapper
{
    public static class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<EmailAddress, string>().ConvertUsing(email => email.Value);
                CreateMap<string, EmailAddress>().ConstructUsing(value => EmailAddress.BuildEmail(value).Value!);
                CreateMap<Name, string>().ConvertUsing(name => name.Value);
                CreateMap<string, Name>().ConstructUsing(value => Name.BuildName(value).Value!);
                CreateMap<Password, string>().ConvertUsing(password => password.Value);
                CreateMap<string, Password>().ConstructUsing(value => Password.BuildPassword(value).Value!);

                CreateMap<User, UserDto>()
                    .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Value));

                CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => EmailAddress.BuildEmail(src.EmailAddress).Value!))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => Name.BuildName(src.FirstName).Value!))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => Name.BuildName(src.LastName).Value!))
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Password.BuildPassword(src.Password).Value!));

                CreateMap<User, UserRegistrationResponseDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<RoleDto, Role>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => (RoleNames)Enum.Parse(typeof(RoleNames), src.Name)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<Role, RoleDto>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => nameof(src.Name)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));
            }
        }
    }
}
