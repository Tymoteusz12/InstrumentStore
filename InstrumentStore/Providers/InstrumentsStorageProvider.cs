using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Providers
{
    public class InstrumentsStorageProvider : IInstrumentsStorageProvider
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        public InstrumentsStorageProvider(
            IUnitOfWork db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<InstrumentDTO>> GetAll()
        {
            var instruments = await _db.Instruments.GetAllAsync(x => x.Brand);
            return _mapper.Map<IEnumerable<InstrumentDTO>>(instruments);
        }

        public async Task<InstrumentDTO> GetById(int id)
        {
            var instrument = await _db.Instruments.GetByIdAsync(id, x => x.Brand);
            return _mapper.Map<InstrumentDTO>(instrument);
        }

        public async Task<InstrumentDTO> Insert(InstrumentDTO model)
        {
            var instrument = _mapper.Map<Instrument>(model);
            await _db.Instruments.Add(instrument);
            return _mapper.Map<InstrumentDTO>(instrument);
        }

        public void Replace(InstrumentDTO model)
        {
            var instrument = _mapper.Map<Instrument>(model);
            _db.Instruments.Edit(instrument);
        }

        public async Task Delete(int id)
        {
            var instrument = await _db.Instruments.GetByIdAsync(id);
            _db.Instruments.Remove(instrument);
        }
    }
}
