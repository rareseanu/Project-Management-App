using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Repositories
{
    public class CheckItemEntityRepository : BaseRepository<CheckItemEntity>
    {
        public CheckItemEntityRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {

        }
        public CheckItemEntity GetById(int id)
        {
            var result = Get(p => p.Id == id);
            return result.FirstOrDefault();
        }
    }
}