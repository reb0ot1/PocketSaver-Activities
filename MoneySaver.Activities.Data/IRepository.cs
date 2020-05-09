﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneySaver.Activities.Data
{
    public interface IRepository<TEntity>
    where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        void Update(int id, TEntity entity);
    }
}
