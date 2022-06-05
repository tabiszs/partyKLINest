using AutoMapper;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Models;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.MinRating, opt => opt.MapFrom(src => src.MinCleanerRating))
                .ForMember(dest => dest.OpinionFromCleaner, opt => opt.MapFrom(src => src.CleanersOpinion))
                .ForMember(dest => dest.OpinionFromClient, opt => opt.MapFrom(src => src.ClientsOpinion))
                .ReverseMap();

            CreateMap<Address, AddressDTO>()
                .ForMember(dest => dest.FlatNo, opt => opt.MapFrom(src => src.FlatNumber))
                .ForMember(dest => dest.BuildingNo, opt => opt.MapFrom(src => src.BuildingNumber))
                .ReverseMap();

            CreateMap<Opinion, OpinionDTO>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.AdditionalInfo))
                .ReverseMap();

            CreateMap<UserInfo, UserInfoDTO>()
                .ReverseMap();
        }
    }
}
