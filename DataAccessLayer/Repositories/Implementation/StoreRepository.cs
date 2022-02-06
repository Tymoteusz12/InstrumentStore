using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Store> GetUserStoreAsync(int userId)
        {
            var stores = await GetAll();
            return stores.Where(store => store.UserId == userId).FirstOrDefault();
        }

        public async Task InsertItemToUserStoreAsync(int userId, Instrument item)
        {
            var store = await GetUserStoreAsync(userId);
            store.StoreItems.Add(item);
            Edit(store);
        }
    }
}
