using Application.Payments;
using AutoMapper;
using Domain.Payments;

namespace Infrastructure.Automapper.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value));

            CreateMap<PaymentDto, Payment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => PaymentName.BuildName(src.Id).Value!))
                .ForMember(dest => dest.Objectives, opt => opt.Ignore());
        }
    }
}
