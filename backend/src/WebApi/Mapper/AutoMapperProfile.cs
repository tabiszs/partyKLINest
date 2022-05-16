using AutoMapper;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.OpinionFromCleaner, opt => opt.MapFrom(src => src.CleanersOpinion))
                .ForMember(dest => dest.OpinionFromClient, opt => opt.MapFrom(src => src.ClientsOpinion))
                .ReverseMap();
        }
    }
}
