using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IStoreService
    {
        Task InsertInstrumentAsync(int storeId, StoreItemDTO storeItem);
        Task RemoveInstrumentFromStoreAsync(int storeId, int instrumentId);
        Task<StoreDTO> GetStoreAsync(int storeId);
    }
}
