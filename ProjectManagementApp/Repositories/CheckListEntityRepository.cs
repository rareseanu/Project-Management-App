using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class CheckListEntityRepository : BaseRepository<CheckListEntity>
    {
        public CheckListEntityRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {

        }
        public CheckListEntity GetById(int id)
        {
            var result = Get(p => p.Id == id);
            return result.FirstOrDefault();
        }
    }
}
