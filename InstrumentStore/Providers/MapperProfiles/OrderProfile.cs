using AutoMapper;
using DataAccessLayer.Models;
using InstrumentStore.Models.DTO;

namespace InstrumentsShop.Providers.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
