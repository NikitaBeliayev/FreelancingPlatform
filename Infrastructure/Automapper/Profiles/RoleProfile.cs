using Application.Roles;
using AutoMapper;
using Domain.Roles;

namespace Infrastructure.Automapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));

            CreateMap<RoleDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => RoleName.BuildRoleName(src.Id).Value!))
                .ForMember(dest => dest.Users, opt => opt.Ignore());
        }
    }
}
