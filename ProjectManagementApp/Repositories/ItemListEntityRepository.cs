using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Repositories
{
    public class ItemListEntityRepository : BaseRepository<ItemListEntity>
    {
        public ItemListEntityRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {

        }
        public ItemListEntity GetById(int id)
        {
            var result = Get(p => p.Id == id);
            return result.FirstOrDefault();
        }
    }
}