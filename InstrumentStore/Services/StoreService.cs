using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreStorageProvider _storeProvider;
        public StoreService(IStoreStorageProvider storeProvider)
        {
            _storeProvider = storeProvider ?? throw new ArgumentNullException(nameof(storeProvider));
        }
        public async Task<StoreDTO> GetUserStoreAsync(string userId)
        {
            var userStore = await _storeProvider.GetUserStoreAsync(userId);

            if (userStore == null)
            {
                throw new ArgumentNullException("User store not found.");
            }

            return userStore;
        }

        public async Task InsertInstrumentAsync(string userId, InstrumentDTO instrument)
        {
            var userStore = await GetUserStoreAsync(userId);
            userStore.StoreItems.Add(instrument);
            _storeProvider.Replace(userStore);
        }

        public async Task RemoveInstrumentFromStoreAsync(string userId, int instrumentId)
        {
            var userStore = await GetUserStoreAsync(userId);
            var deletedItem = userStore.StoreItems.Where(item => item.Id == instrumentId).FirstOrDefault();

            if(deletedItem == null)
            {
                throw new ArgumentNullException("Invalid item id.");
            }

            userStore.StoreItems = userStore.StoreItems.Where(item => item.Id != instrumentId).ToList();
            _storeProvider.Replace(userStore);
        }
    }
}
