using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;
using System.Threading.Tasks;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IStoreStorageProvider : IStorageProvider<StoreDTO>
    {
        Task<StoreDTO> AddItemToStoreAsync(int storeId, StoreItemDTO storeItemDTO);
        Task DeleteStoreItem(int storeItemId);
    }
}
