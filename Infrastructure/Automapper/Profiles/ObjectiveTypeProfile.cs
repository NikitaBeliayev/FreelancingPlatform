using Application.Objectives.Types.ResponseDto;
using Application.Objectives.Types;
using AutoMapper;
using Domain.Types;

namespace Infrastructure.Automapper.Profiles
{
    public class ObjectiveTypeProfile : Profile
    {
        public ObjectiveTypeProfile()
        {
            CreateMap<ObjectiveType, ResponseTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.TypeTitle.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<TypeDto, ObjectiveType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.TypeTitle, opt => opt.MapFrom(src => ObjectiveTypeTitle.BuildObjectiveTypeTitle(src.Id).Value!));
        }
    }
}
