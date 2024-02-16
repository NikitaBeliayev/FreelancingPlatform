using Application.Objectives;
using Application.Objectives.Category;
using Application.Objectives.ObjectiveStatus;
using Application.Objectives.ObjectiveTypes;
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
                    .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => EmailAddress.BuildEmail(src.EmailAddress).Value!))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => Name.BuildName(src.FirstName).Value!))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => Name.BuildName(src.LastName).Value!))
                    .ForMember(dest => dest.Password,
                        opt => opt.MapFrom(src => Password.BuildPassword(src.Password).Value!));

                CreateMap<User, UserRegistrationResponseDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<RoleDto, Role>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => (RoleNameType)Enum.Parse(typeof(RoleNameType), src.Name)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<Role, RoleDto>()
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => nameof(src.Name)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<ObjectiveTypeDto, ObjectiveType>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TypeTitle,
                        opt => opt.MapFrom(src =>
                            (ObjectiveTypeVariations)Enum.Parse(typeof(ObjectiveTypeVariations), src.TypeTitle)))
                    .ForMember(dest => dest.Eta, opt => opt.MapFrom(src => src.ETA))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));

                CreateMap<ObjectiveType, ObjectiveTypeDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TypeTitle,
                        opt => opt.MapFrom(src => nameof(src.TypeTitle)))
                    .ForMember(dest => dest.ETA, opt => opt.MapFrom(src => src.Eta))
                    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration));

                CreateMap<ObjectiveStatusDto, ObjectiveStatus>()
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src =>
                            (ObjectiveStatusTitleType)Enum.Parse(typeof(ObjectiveStatusTitleType), src.Title)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<ObjectiveStatus, ObjectiveStatusDto>()
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => nameof(src.Title)))
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id));

                CreateMap<PaymentDto, Payment>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src
                            => (PaymentType)Enum.Parse(typeof(PaymentType), src.Name)));

                CreateMap<Payment, PaymentDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => nameof(src.Name)));

                CreateMap<Category, CategoryDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => src.Title));

                CreateMap<CategoryDto, Category>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => CategoryName.BuildCategoryName(src.Title)));

                CreateMap<Objective, ObjectiveDto>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ObjectiveStatus,
                        opt => opt.MapFrom(src => new ObjectiveStatusDto()
                        {
                            Id = src.ObjectiveStatusId,
                            Title = src.ObjectiveStatus.Title.Value
                        }))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => src.Title.Value))
                    .ForMember(dest => dest.Categories,
                        opt => opt.MapFrom(src => src.Categories))
                    .ForMember(dest => dest.Description,
                        opt => opt.MapFrom(src => src.Description.Value))
                    .ForMember(dest => dest.Payment,
                        opt => opt.MapFrom(src => new PaymentDto()
                        {
                            Id = src.ObjectiveStatus.Id,
                            Name = src.Payment.Name.Value
                        }))
                    .ForMember(dest => dest.Attachments,
                        opt => opt.MapFrom(src => src.Attachments))
                    .ForMember(dest => dest.PaymentAmount,
                        opt => opt.MapFrom(src => src.PaymentAmount))
                    .ForMember(dest => dest.Type,
                        opt => opt.MapFrom(src => new ObjectiveTypeDto()
                        {
                            Duration = src.Type.Duration,
                            ETA = src.Type.Eta,
                            Id = src.Type.Id,
                            TypeTitle = src.Type.TypeTitle.Title
                        }));

                CreateMap<ObjectiveDto, Objective>()
                    .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Categories,
                        opt => opt.MapFrom(src
                            => src.Categories.Select(c => new Category()
                            {
                                Id = c.Id,
                                Objectives = new List<Objective>(),
                                Title = CategoryName.BuildCategoryName(c.Title).Value!
                            })))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => ObjectiveTitle.BuildName(src.Title).Value!))
                    .ForMember(dest => dest.Attachments,
                        opt => opt.MapFrom(src => src.Attachments))
                    .ForMember(dest => dest.Description,
                        opt => opt.MapFrom(src => ObjectiveDescription.BuildName(src.Description).Value!))
                    .ForMember(dest => dest.Payment,
                        opt => opt.MapFrom(src => new Payment()
                        {
                            Id = src.Payment.Id,
                            Name = PaymentName.BuildName(src.Payment.Id).Value!,
                            Objectives = new List<Objective>()
                        }))
                    .ForMember(dest => dest.ObjectiveStatus,
                        opt => opt.MapFrom(src => new ObjectiveStatus(
                            src.ObjectiveStatus.Id,
                            ObjectiveStatusTitle.BuildStatusTitle(src.ObjectiveStatus.Id).Value!,
                            new List<Objective>())))
                    .ForMember(dest => dest.Type,
                        opt => opt.MapFrom(src => new ObjectiveType(
                            src.Type.Id, new List<Objective>(),
                            ObjectiveTypeTitle.BuildObjectiveTypeTitle(src.Type.Id).Value!, src.Type.ETA,
                            src.Type.Duration)))
                    .ForMember(dest => dest.PaymentAmount,
                        opt => opt.MapFrom(src => src.PaymentAmount));
            }
        }
    }
}
