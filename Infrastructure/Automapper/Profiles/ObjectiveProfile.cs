using Application.Objectives.Categories;
using Application.Users;
using AutoMapper;
using Domain.Objectives;
using Application.Objectives.Types.ResponseDto;
using Application.Objectives.ResponseDto;
using Application.Objectives.Categories.ResponseDto;
using Application.Objectives.RequestDto;

namespace Infrastructure.Automapper.Profiles
{
    public class ObjectiveProfile : Profile
    {
        public ObjectiveProfile()
        {
            CreateMap<Objective, ResponseObjectiveDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Categories.Select(t => new CategoryDto { Id = t.Id, Title = t.Title.Value })))
                .ForMember(dest => dest.CreatorPublicContacts, opt => opt.MapFrom(src => src.CreatorPublicContacts))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => new UserDto { Id = src.Creator.Id, Email = src.Creator.Email.Value, FirstName = src.Creator.FirstName.Value, LastName = src.Creator.LastName.Value }))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new ResponseTypeDto { Id = src.Type.Id, Title = src.Type.TypeTitle.Title, Description = src.Type.Description }))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.Value));

            CreateMap<Objective, ObjectiveCreateDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Categories.Select(t => new SimpleCategoryResponseDto { Id = t.Id })))
                .ForMember(dest => dest.CreatorPublicContacts, opt => opt.MapFrom(src => src.CreatorPublicContacts))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new SimpleResponseTypeDto { Id = src.Type.Id }))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.Value));

            CreateMap<Objective, TaskForYouDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Categories.Select(t => new CategoryDto { Id = t.Id, Title = t.Title.Value })))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new ResponseTypeDto { Id = src.Type.Id, Title = src.Type.TypeTitle.Title, Description = src.Type.Description }))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.Value));

            CreateMap<Objective, GetObjectiveResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                .ForMember(dest => dest.CreatorPublicContacts, opt => opt.MapFrom(src => src.CreatorPublicContacts))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Categories.Select(t => new CategoryDto { Id = t.Id, Title = t.Title.Value })))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => new ResponseTypeDto { Id = src.Type.Id, Title = src.Type.TypeTitle.Title, Description = src.Type.Description }))
                .ForMember(dest => dest.Deadline, opt => opt.MapFrom(src => src.Deadline.Value))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => new UserDto { Id = src.Creator.Id, Email = src.Creator.Email.Value, FirstName = src.Creator.FirstName.Value, LastName = src.Creator.LastName.Value }))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
