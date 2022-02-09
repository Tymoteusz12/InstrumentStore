using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories.Implementation
{
    public class StoreItemRepository : Repository<StoreItem>, IStoreItemRepository
    {
        public StoreItemRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
