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
    public class BoardUserEntityRepository
    {
        protected readonly ProjectManagementDbContext dbContext;
        protected readonly DbSet<BoardUserEntity> table;

        public BoardUserEntityRepository(ProjectManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<BoardUserEntity>();
        }

        public IQueryable<BoardUserEntity> Get(Expression<Func<BoardUserEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return table.Where(predicate);

            return table;
        }

        public IQueryable<BoardUserEntity> GetAll(Expression<Func<BoardUserEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return table
                    .Where(predicate);

            return table;
        }

        public async Task<int> CountAll(Expression<Func<BoardUserEntity, bool>> predicate = null)
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

        public async Task<BoardUserEntity> Create(BoardUserEntity entity, bool commit = true)
        {
            await table.AddAsync(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<BoardUserEntity> Update(BoardUserEntity entity, bool commit = true)
        {
            table.Update(entity);

            if (commit)
                await Commit();

            return entity;
        }

        public async Task<BoardUserEntity> Delete(BoardUserEntity entity, bool commit = true)
        {
            table.Remove(entity);

            if (commit)
                await Commit();

            return entity;
        }
    }
}
