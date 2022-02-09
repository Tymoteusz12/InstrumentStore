using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Providers
{
    public class StoreStorageProvider : IStoreStorageProvider
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public StoreStorageProvider(IUnitOfWork db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<StoreDTO>> GetAll()
        {
            var stores = await _db.Store.GetAllAsync(x => x.StoreItems, x => x.User);
            return _mapper.Map<IEnumerable<StoreDTO>>(stores);
        }

        public async Task<StoreDTO> GetById(int id)
        {
            var store = await _db.Store.GetByIdAsync(id, x => x.StoreItems, x => x.User);
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<StoreDTO> AddItemToStoreAsync(int storeId, StoreItemDTO storeItemDTO)
        {
            var store = await GetById(storeId);
            if(store.StoreItems.Count < 10)
            {
                store.StoreItems.Add(storeItemDTO);
                store.FinalPrice += storeItemDTO.Price;
                Replace(store);
            }
            return store;
        }
        public async Task<StoreDTO> Insert(StoreDTO model)
        {
            var store = _mapper.Map<Store>(model);
            await _db.Store.Add(store);
            return _mapper.Map<StoreDTO>(store);
        }

        public void Replace(StoreDTO model)
        {
            var store = _mapper.Map<Store>(model);
            _db.Store.Edit(store);
        }

        public async Task DeleteStoreItem(int storeItemId)
        {
            var storeItem = await _db.StoreItems.GetByIdAsync(storeItemId);
            _db.StoreItems.Remove(storeItem);
        }
        public async Task Delete(int id)
        {
            var store = await _db.Store.GetByIdAsync(id);
            _db.Store.Remove(store);
        }
    }
}
