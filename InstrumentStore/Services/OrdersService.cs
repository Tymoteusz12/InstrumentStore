using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersStorageProvider _ordersProvider;
        private readonly IStoreStorageProvider _storeProvider;
        public OrdersService(IOrdersStorageProvider ordersProvider, IStoreStorageProvider storeProvider)
        {
            _ordersProvider = ordersProvider ?? throw new ArgumentNullException(nameof(ordersProvider));
            _storeProvider = storeProvider ?? throw new ArgumentNullException(nameof(storeProvider));
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _ordersProvider.GetAll();
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userId)
        {
            var orders = await GetAllOrdersAsync();

            return orders.Where(order => order.UserId == userId).ToList();
        }
        public async Task<OrderDTO> MakeOrderAsync(OrderDTO model)
        {
            var store = (await _storeProvider.GetAll()).Where(store => store.UserId == model.UserId).FirstOrDefault();
            
            if(store == null)
            {
                throw new ArgumentNullException("Invalid store userId.");
            }

            store.StoreItems.ForEach(item => _storeProvider.DeleteStoreItem(item.StoreItemId).Wait());
            store.FinalPrice = 0;
            store.StoreItems.Clear();
            _storeProvider.Replace(store);
            return await _ordersProvider.Insert(model);
        }
    }
}
