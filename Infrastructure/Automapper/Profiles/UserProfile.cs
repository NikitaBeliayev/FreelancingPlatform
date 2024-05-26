using Application.Models.Jwt;
using Application.Users.ResponseDto;
using Application.Users;
using AutoMapper;
using Domain.Users.UserDetails;
using Domain.Users;

namespace Infrastructure.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Email, string>().ConvertUsing(email => email.Value);
            CreateMap<string, Email>().ConstructUsing(value => Email.BuildEmail(value).Value!);
            CreateMap<Name, string>().ConvertUsing(name => name.Value);
            CreateMap<string, Name>().ConstructUsing(value => Name.BuildName(value).Value!);
            CreateMap<Password, string>().ConvertUsing(password => password.Value);
            CreateMap<string, Password>().ConstructUsing(value => Password.BuildPassword(value).Value!);

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Value));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => Email.BuildEmail(src.Email).Value!))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => Name.BuildName(src.FirstName).Value!))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => Name.BuildName(src.LastName).Value!))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Password.BuildPassword(src.Password).Value!));

            CreateMap<User, UserRegistrationResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, UserEmailConfirmationResponseDto>();
            CreateMap<User, ResetPasswordResponseDto>();
            CreateMap<Tuple<User, JwtCredentials>, UserLoginResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
                .ForMember(dest => dest.Credentials, opt => opt.MapFrom(src => src.Item2));
            CreateMap<User, UserResendEmailConfirmationResponseDto>();
        }
    }
}
