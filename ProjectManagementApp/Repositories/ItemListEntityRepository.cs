using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Repositories
{
    public class ItemListEntityRepository
    {
        private readonly ProjectManagementDbContext _dbContext;

        public ItemListEntityRepository(ProjectManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ItemListEntity>> Search(string text)
        {
            return await _dbContext.ItemLists
                .Where(p => p.Title.Contains(text))
                .ToListAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var list = await GetById(id);

            if (list != null)
            {
                _dbContext.ItemLists.Remove(list);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<ItemListEntity>> GetAll()
        {
            return await _dbContext.ItemLists.ToListAsync();
        }

        public async Task<ItemListEntity> GetById(int id)
        {
            var result = await _dbContext.ItemLists.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<ItemListEntity> Update(ItemListEntity list)
        {
            var result = _dbContext.ItemLists.Update(list);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ItemListEntity> Insert(ItemListEntity list)
        {
            EntityEntry<ItemListEntity> result = await _dbContext.ItemLists.AddAsync(list);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}