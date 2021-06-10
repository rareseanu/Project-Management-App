using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class BoardEntityRepository : BaseRepository<BoardEntity>
    {
        public BoardEntityRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {

        }
        public BoardEntity GetById(int id)
        {
            var result = Get(p => p.Id == id);
            return result.FirstOrDefault();
        }
    }
}