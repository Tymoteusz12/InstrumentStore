using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

        IEnumerable<OrderDTO> GetUserOrders(string userId);
        Task<OrderDTO> MakeOrderAsync(OrderDTO model);
    }
}
