using DataAccessLayer.Models;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetUserStoreAsync(int userId);
        Task InsertItemToUserStoreAsync(int userId, Instrument item);
    }
}
