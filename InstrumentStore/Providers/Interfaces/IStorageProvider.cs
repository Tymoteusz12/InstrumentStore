﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstrumentStore.Providers.Interfaces
{
    public interface IStorageProvider<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T model);
        void Replace(T model);
        Task Delete(int id);
    }
}
