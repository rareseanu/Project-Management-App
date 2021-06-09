using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class CommentaryEntity : BaseEntity
    {
        public int ItemEntityId { get; set; }
        public ItemEntity ItemEntity { get; set; }

        [ForeignKey("CreatedBy")]
        public UserEntity UserEntity { get; set; }
    }
}
