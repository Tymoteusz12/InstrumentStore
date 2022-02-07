using AutoMapper;
using DataAccessLayer.Models;
using InstrumentStore.Models.DTO;

namespace InstrumentsShop.Providers.MapperProfiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreDTO, Store>();
            CreateMap<Store, StoreDTO>();
        }
    }
}
