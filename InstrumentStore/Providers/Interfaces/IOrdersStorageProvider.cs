using InstrumentStore.Models.DTO;
using System.Collections.Generic;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IOrdersStorageProvider : IStorageProvider<OrderDTO>
    {
        IEnumerable<OrderDTO> GetUserOrders(string userId);
    }
}
