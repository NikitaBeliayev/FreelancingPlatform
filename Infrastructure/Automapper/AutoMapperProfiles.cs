using Application.Models.Jwt;
using Application.Objectives;
using Application.Objectives.Categories;
using Application.Objectives.ObjectiveStatus;
using Application.Objectives.Types;
using Application.Roles;
using Application.Payments;
using Application.Users;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.Categories;
using Domain.Objectives;
using Domain.Roles;
using Domain.Payments;
using Domain.Statuses;
using Domain.Types;
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
                CreateMap<Email, string>().ConvertUsing(email => email.Value);
                CreateMap<string, Email>().ConstructUsing(value => Email.BuildEmail(value).Value!);
                CreateMap<Name, string>().ConvertUsing(name => name.Value);
                CreateMap<string, Name>().ConstructUsing(value => Name.BuildName(value).Value!);
                CreateMap<Password, string>().ConvertUsing(password => password.Value);
                CreateMap<string, Password>().ConstructUsing(value => Password.BuildPassword(value).Value!);
                
                //mapping between user and all response dto
                CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Value));

                CreateMap<UserDto, User>()
                    .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => Email.BuildEmail(src.Email).Value!))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => Name.BuildName(src.FirstName).Value!))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => Name.BuildName(src.LastName).Value!))
                    .ForMember(dest => dest.Password,
                        opt => opt.MapFrom(src => Password.BuildPassword(src.Password).Value!));

                CreateMap<User, UserRegistrationResponseDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));
                
                CreateMap<User, UserEmailConfirmationResponseDto>();
                
                CreateMap<User, ResetPasswordResponseDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));
                
                CreateMap<Tuple<User, JwtCredentials>, UserLoginResponseDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Item1.Id))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Item2));
                
                CreateMap<User, UserResendEmailConfirmationResponseDto>();
                
                //mapping between role and all response dto
                CreateMap<Role, RoleDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));
                
                CreateMap<RoleDto, Role>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => RoleName.BuildRoleName(src.Id).Value!))
                    .ForMember(dest => dest.Users, opt => opt.Ignore());
                
                
                //mapping between payment and all response dto
                CreateMap<Payment, PaymentDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));
                
                CreateMap<PaymentDto, Payment>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => 
                        PaymentName.BuildName(PaymentVariations.GetValue(src.Id).Value!).Value!))
                    .ForMember(dest => dest.Objectives, opt => opt.Ignore());
                
                //mapping between objective type and all response dto

                CreateMap<ObjectiveType, TypeDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                    .ForMember(dest => dest.TypeTitle, opt => opt.MapFrom(src => src.TypeTitle.Title));
                
                CreateMap<TypeDto, ObjectiveType>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                    .ForMember(dest => dest.TypeTitle, opt =>
                        opt.MapFrom(src =>
                            ObjectiveTypeTitle.BuildObjectiveTypeTitle(src.TypeTitle).Value!));
                
                
                
                //mapping between objective type and all response dto
                CreateMap<Category, CategoryDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value));

                CreateMap<CategoryDto, Category>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => CategoryName.BuildCategoryNameWithoutValidation(src.Title!)));
            }
        }
    }
}
