using InstrumentsShop.Models.DTO;
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
        public async Task<StoreDTO> GetStoreAsync(int storeId)
        {
            var userStore = await _storeProvider.GetById(storeId);

            if (userStore == null)
            {
                throw new ArgumentNullException("User store not found.");
            }

            return userStore;
        }

        public async Task InsertInstrumentAsync(int storeId, StoreItemDTO storeItem)
        {
            var userStore = await GetStoreAsync(storeId);

            await _storeProvider.AddItemToStoreAsync(storeId, storeItem);
        }

        public async Task RemoveInstrumentFromStoreAsync(int storeId, int instrumentId)
        {
            var userStore = await GetStoreAsync(storeId);
            var deletedItem = userStore.StoreItems.Where(item => item.InstrumentId == instrumentId).FirstOrDefault();

            if(deletedItem == null)
            {
                throw new ArgumentNullException("Invalid item id.");
            }
            userStore.FinalPrice -= deletedItem.Price;
            userStore.StoreItems = userStore.StoreItems.Where(item => item.InstrumentId != instrumentId).ToList();
            await _storeProvider.DeleteStoreItem(deletedItem.StoreItemId);
            _storeProvider.Replace(userStore);
        }
    }
}
