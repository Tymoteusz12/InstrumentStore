using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Services.Providers
{
    public class OrdersStorageProvider : IOrdersStorageProvider
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        public OrdersStorageProvider(
            IUnitOfWork db,
            IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
       
        public async Task<IEnumerable<OrderDTO>> GetAll()
        {
            var orders = await _db.Orders.GetAll();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetById(int id)
        {
            var order = await _db.Orders.GetById(id);
            return _mapper.Map<OrderDTO>(order);
        }

        public IEnumerable<OrderDTO> GetUserOrders(string userId)
        {
            var user = _db.GetAllUsers().Where(user => user.Id == userId).FirstOrDefault();
            return _mapper.Map<IEnumerable<OrderDTO>>(user.Orders);
        }

        public async Task<OrderDTO> Insert(OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            await _db.Orders.Add(order);
            await _db.Save();
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task Replace(OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            _db.Orders.Edit(order);
            await _db.Save();
        }

        public async Task Delete(int id)
        {
            var order = await _db.Orders.GetById(id);
            _db.Orders.Remove(order);
            await _db.Save();
        }
    }
}
