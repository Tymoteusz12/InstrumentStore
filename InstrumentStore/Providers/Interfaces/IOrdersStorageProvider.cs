using InstrumentStore.Models.DTO;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IOrdersStorageProvider : IStorageProvider<OrderDTO>
    {
        UserDTO GetUserAsync(string userId);
    }
}
