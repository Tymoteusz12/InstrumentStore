using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersStorageProvider _ordersProvider;
        public OrdersService(IOrdersStorageProvider ordersProvider)
        {
            _ordersProvider = ordersProvider ?? throw new ArgumentNullException();
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _ordersProvider.GetAll();
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userId)
        {
            return _ordersProvider.GetUserOrders(userId);
        }

        public async Task<OrderDTO> MakeOrderAsync(OrderDTO model)
        {
            return await _ordersProvider.Insert(model);
        }
    }
}
