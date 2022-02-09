using InstrumentsShop.Models.DTO;
using InstrumentStore.Models.DTO;
using System.Threading.Tasks;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IOrdersStorageProvider : IStorageProvider<OrderDTO>
    {
        UserDTO GetUserAsync(string userId);
    }
}
