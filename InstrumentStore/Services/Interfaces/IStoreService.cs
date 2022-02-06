using InstrumentStore.Models.DTO;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IStoreService
    {
        Task InsertInstrumentAsync(string userId, InstrumentDTO instrument);
        Task RemoveInstrumentFromStoreAsync(string userId, int instrumentId);
        Task<StoreDTO> GetUserStoreAsync(string userId);
    }
}
