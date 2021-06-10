using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class ItemLabelEntity
    {
        public int Id { get; set; }
        public int? ItemEntityId { get; set; }
        public ItemEntity ItemEntity { get; set; }
        public int LabelEntityId { get; set; }
        public LabelEntity LabelEntity { get; set; }
    }
}
