using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Repositories
{
    public class CommentaryEntityRepository : BaseRepository<CommentaryEntity>
    {
        public CommentaryEntityRepository(ProjectManagementDbContext dbContext) : base(dbContext)
        {

        }
        public CommentaryEntity GetById(int id)
        {
            var result = Get(p => p.Id == id);
            return result.FirstOrDefault();
        }
    }
}