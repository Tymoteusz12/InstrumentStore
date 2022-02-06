using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Providers
{
    public class StoreStorageProvider : IStoreStorageProvider
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<StoreDTO>> GetAll()
        {
            var stores = await _db.Store.GetAll();
            return _mapper.Map<IEnumerable<StoreDTO>>(stores);
        }

        public async Task<StoreDTO> GetById(int id)
        {
            var store = await _db.Store.GetById(id);
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<StoreDTO> GetUserStoreAsync(string userId)
        {
            var stores = await GetAll();
            var userStore = stores.Where(store => store.UserId == userId).FirstOrDefault();
            return userStore;
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

        public async Task Delete(int id)
        {
            var store = await _db.Store.GetById(id);
            _db.Store.Remove(store);
        }
    }
}
