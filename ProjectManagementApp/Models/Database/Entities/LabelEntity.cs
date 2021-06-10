using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class LabelEntity : BaseEntity
    {
        public string Text { get; set; }
        public int BoardEntityId { get; set; }
        public BoardEntity BoardEntity{get;set;}
        public List<ItemLabelEntity> ItemList { get; set; }
    }
}
