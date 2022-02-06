using DataAccessLayer.Models;
using InstrumentStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IStoreStorageProvider : IStorageProvider<StoreDTO>
    {
        Task<StoreDTO> GetUserStoreAsync(string userId);
    }
}
