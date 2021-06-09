using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class CheckListEntity : BaseEntity
    {
        public List<CheckItemEntity> CheckItems { get; set; }
        public int ItemEntityId { get; set; }
        public ItemEntity ItemEntity { get; set; }
    }
}
