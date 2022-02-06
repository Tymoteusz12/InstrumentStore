﻿using InstrumentStore.Models.DTO;
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
        public InstrumentsService(IInstrumentsStorageProvider instrumentsProvider)
        {
            _instrumentsProvider = instrumentsProvider ?? throw new ArgumentNullException(nameof(instrumentsProvider));
        }
        public async Task<IEnumerable<InstrumentDTO>> GetAllInstrumentsAsync()
        {
            return await _instrumentsProvider.GetAll();
        }
        public async Task<InstrumentDTO> GetByIdAsync(int id)
        {
            var instrument = await _instrumentsProvider.GetById(id);

            if (instrument == null)
            {
                throw new ArgumentNullException("Invalid id.");
            }

            return instrument;
        }
        public async Task<InstrumentDTO> InsertInstrumentAsync(InstrumentDTO model)
        {
            return await _instrumentsProvider.Insert(model);
        }

        public void EditInstrument(InstrumentDTO model)
        {
            _instrumentsProvider.Replace(model);
        }

        public async Task RemoveInstrumentAsync(int id)
        {
            await _instrumentsProvider.GetById(id);
            await _instrumentsProvider.Delete(id);
        }

        
    }
}
