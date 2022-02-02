using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Brands = new BrandRepository(_context);
            Instruments = new InstrumentsRepository(_context);
            Orders = new OrderRepository(_context);
        }

        private readonly ApplicationDbContext _context;
        public IBrandRepository Brands { get; private set; }

        public IInstrumentsRepository Instruments { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ApplicationUser> GetAllUsers() => _context.Users;
    }
}
