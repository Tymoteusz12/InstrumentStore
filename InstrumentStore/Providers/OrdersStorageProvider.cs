using AutoMapper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentStore.Models.DTO;
using InstrumentStore.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Providers
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
            var orders = await _db.Orders.GetAllAsync(x => x.ApplicationUser);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetById(int id)
        {
            var order = await _db.Orders.GetByIdAsync(id, x => x.ApplicationUser);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> Insert(OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            await _db.Orders.Add(order);
            return _mapper.Map<OrderDTO>(order);
        }

        public void Replace(OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            _db.Orders.Edit(order);
        }

        public async Task Delete(int id)
        {
            var order = await _db.Orders.GetByIdAsync(id);
            _db.Orders.Remove(order);
        }

        public UserDTO GetUserAsync(string userId)
        {
            var user = _db.GetAllUsers().Where(user => user.Id == userId).FirstOrDefault();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
