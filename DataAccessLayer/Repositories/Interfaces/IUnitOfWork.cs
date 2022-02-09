using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBrandRepository Brands { get; }
        IInstrumentsRepository Instruments { get; }
        IOrderRepository Orders { get; }
        IStoreRepository Store { get; }
        IStoreItemRepository StoreItems { get; }
        IOrderItemRepository OrderItems { get; }
        IEnumerable<ApplicationUser> GetAllUsers();
    }
}
