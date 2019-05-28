namespace LoWaiLo.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LoWaiLo.Data.Common;
    using Microsoft.EntityFrameworkCore;

    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly LoWaiLoDbContext context;

        private readonly DbSet<TEntity> dbSet;

        public DbRepository(LoWaiLoDbContext context)
        {
            this.context = context;

            this.dbSet = this.context.Set<TEntity>();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public IQueryable<TEntity> AllAsNoTracking()
        {
            return this.dbSet.AsNoTracking();
        }

        public Task<TEntity> GetByIdAsync(params object[] id)
        {
            return this.dbSet.FindAsync(id);
        }

        public void AddAsync(TEntity entity)
        {
            this.dbSet.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return this.dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        Task IRepository<TEntity>.AddAsync(TEntity entities)
        {
            return this.dbSet.AddRangeAsync(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this.dbSet.RemoveRange(entities);
        }
    }
}
