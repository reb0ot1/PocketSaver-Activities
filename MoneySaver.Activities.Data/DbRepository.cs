using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MoneySaver.Activities.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
       where TEntity : class
    {
        private DbSet<TEntity> dbSet;
        private readonly ActivitiesContext context;
        public DbRepository(ActivitiesContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await this.dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                ;
            }
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public void Update(int id, TEntity entity)
        {
            this.dbSet.Update(entity);
        }
    }
}
