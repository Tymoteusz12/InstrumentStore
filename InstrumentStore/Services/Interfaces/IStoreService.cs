using InstrumentStore.Models.DTO;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IStoreService
    {
        Task InsertInstrumentAsync(int storeId, InstrumentDTO instrument);
        Task RemoveInstrumentFromStoreAsync(int storeId, int instrumentId);
        Task<StoreDTO> GetStoreAsync(int storeId);
    }
}
