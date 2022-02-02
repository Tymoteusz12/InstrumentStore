using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services
{
    public class InstrumentsService : IInstrumentsService
    {
        private readonly IInstrumentsStorageProvider _instrumentsProvider;

        public async Task<IEnumerable<InstrumentDTO>> GetAllInstrumentsAsync()
        {
            return await _instrumentsProvider.GetAll();
        }

        public async Task<InstrumentDTO> InsertInstrumentAsync(InstrumentDTO model)
        {
            return await _instrumentsProvider.Insert(model);
        }

        public async Task EditInstrumentAsync(InstrumentDTO model)
        {
            await _instrumentsProvider.Replace(model);
        }

        public async Task RemoveInstrumentAsync(Guid id)
        {
            await _instrumentsProvider.Delete(id);
        }
    }
}
