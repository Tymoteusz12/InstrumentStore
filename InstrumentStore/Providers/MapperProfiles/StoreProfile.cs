using AutoMapper;
using DataAccessLayer.Models;
using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;

namespace InstrumentsShop.Providers.MapperProfiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreDTO, Store>();
            CreateMap<Store, StoreDTO>();
            CreateMap<StoreItemDTO, StoreItem>().ForMember(dest => dest.Id, src => src.MapFrom(ob => ob.StoreItemId));
            CreateMap<StoreItem, StoreItemDTO>().ForMember(dest => dest.StoreItemId, src => src.MapFrom(ob => ob.Id));
            CreateMap<InstrumentDTO, StoreItemDTO>().ForMember(dest => dest.InstrumentId, src => src.MapFrom(ob => ob.Id));
        }
    }
}
