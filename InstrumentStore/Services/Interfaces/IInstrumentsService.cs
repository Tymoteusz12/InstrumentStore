using DataAccessLayer.Models;
using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IInstrumentsService
    {
        Task<IEnumerable<InstrumentDTO>> GetAllInstrumentsAsync();

        Task<InstrumentDTO> GetByIdAsync(int id);
        Task<InstrumentDTO> InsertInstrumentAsync(InstrumentDTO model);

        void EditInstrument(InstrumentDTO model);

        Task RemoveInstrumentAsync(int id);
    }
}
