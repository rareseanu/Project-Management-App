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
    public class ItemLabelEntityRepository
    {
        protected readonly ProjectManagementDbContext dbContext;
        protected readonly DbSet<ItemLabelEntity> table;

        public ItemLabelEntityRepository(ProjectManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<ItemLabelEntity>();
        }

        public IQueryable<ItemLabelEntity> Get(Expression<Func<ItemLabelEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return table.Where(predicate);

            return table;
        }

        public IQueryable<ItemLabelEntity> GetAll(Expression<Func<ItemLabelEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return table
                    .Where(predicate);

            return table;
        }

        public async Task<int> CountAll(Expression<Func<ItemLabelEntity, bool>> predicate = null)
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

        public async Task<ItemLabelEntity> Create(ItemLabelEntity entity, bool commit = true)
        {
            await table.AddAsync(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<ItemLabelEntity> Update(ItemLabelEntity entity, bool commit = true)
        {
            table.Update(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<ItemLabelEntity> Delete(ItemLabelEntity entity, bool commit = true)
        {
            table.Remove(entity);

            if (commit)
                await Commit();

            return entity;
        }
    }
}
