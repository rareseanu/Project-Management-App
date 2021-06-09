using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly ProjectManagementDbContext dbContext;
        protected readonly DbSet<T> table;

        public BaseRepository(ProjectManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return table.Where(predicate);

            return table;
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await table
                    .Where(predicate)
                    .ToListAsync();

            return await table.ToListAsync();
        }

        public async Task<int> CountAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await table
                    .Where(predicate)
                    .CountAsync();

            return await table.CountAsync();
        }

        public async Task Commit()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<T> Create(T entity, bool commit = true)
        {
            await table.AddAsync(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<T> Update(T entity, bool commit = true)
        {
            table.Update(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<T> Delete(T entity, bool commit = true)
        {
            // Soft delete
            entity.IsDeleted = true;
            table.Update(entity);

            if (commit)
                await Commit();

            return entity;
        }
    }
}
