using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(ApplicationDbContext context) : base(context) { }

        public override void Edit(Store store)
        {
            var local = Context.Set<ApplicationUser>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(store.User.Id));
            // check if local is not null 
            if (local != null)
            {
                // detach
                Context.Entry(local).State = EntityState.Detached;
            }
            Context.Set<Store>().Update(store);
            Context.Entry(store).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
