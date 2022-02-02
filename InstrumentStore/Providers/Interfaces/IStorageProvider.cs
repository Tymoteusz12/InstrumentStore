using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IStorageProvider<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> Insert(T model);
        Task Replace(T model);
        Task Delete(Guid id);
    }
}
