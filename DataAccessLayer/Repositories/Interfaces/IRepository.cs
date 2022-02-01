﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityObject, new()
    {
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity);
        void Remove(TEntity entity);
    }
}