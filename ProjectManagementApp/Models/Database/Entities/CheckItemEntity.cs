using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class CheckItemEntity : BaseEntity
    {
        public string Task { get; set; }
        public bool IsChecked { get; set; }
        public int CheckListEntityId { get; set; }
        public CheckListEntity CheckList { get; set; }
    }
}
