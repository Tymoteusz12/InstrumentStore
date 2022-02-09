using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            ApplicationDbContext context,
            IBrandRepository brandRepository,
            IInstrumentsRepository instrumentsRepository,
            IOrderRepository orderRepository,
            IStoreRepository storeRepository,
            IStoreItemRepository storeItems,
            IOrderItemRepository orderItems)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Brands = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            Instruments = instrumentsRepository ?? throw new ArgumentNullException(nameof(instrumentsRepository));
            Orders = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            Store = storeRepository ?? throw new ArgumentNullException(nameof(storeRepository));
            StoreItems = storeItems ?? throw new ArgumentNullException(nameof(storeItems));

            OrderItems = orderItems ?? throw new ArgumentNullException(nameof(orderItems));
        }

        private readonly ApplicationDbContext _context;
        public IBrandRepository Brands { get; private set; }

        public IInstrumentsRepository Instruments { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IStoreRepository Store { get; private set; }

        public IStoreItemRepository StoreItems { get; private set; }

        public IOrderItemRepository OrderItems { get; private set; }
        public IEnumerable<ApplicationUser> GetAllUsers() => _context.Users.AsNoTracking().ToList();
    }
}
