using AutoMapper;
using DataAccessLayer.Models;
using InstrumentStore.Models.DTO;

namespace InstrumentsShop.Providers.MapperProfiles
{
    public class InstrumentProfile : Profile
    {
        public InstrumentProfile()
        {
            CreateMap<InstrumentDTO, Instrument>();
            CreateMap<Instrument, InstrumentDTO>();
        }
    }
}
