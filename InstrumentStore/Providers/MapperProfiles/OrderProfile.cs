using AutoMapper;
using DataAccessLayer.Models;
using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;

namespace InstrumentsShop.Providers.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Order>().ForMember(dest => dest.ApplicationUserId, src => src.MapFrom(ob => ob.UserId));
            CreateMap<Order, OrderDTO>().ForMember(dest => dest.UserId, src => src.MapFrom(ob => ob.ApplicationUserId));
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<StoreItemDTO, OrderItemDTO>();
            CreateMap<InstrumentDTO, OrderItemDTO>().ForMember(dest => dest.InstrumentId, src => src.MapFrom(ob => ob.Id));
        }
    }
}
