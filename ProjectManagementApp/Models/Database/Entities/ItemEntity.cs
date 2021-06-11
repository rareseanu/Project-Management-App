using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class ItemEntity : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public CheckListEntity CheckLists { get; set; }
        public List<CommentaryEntity> CommentaryList { get; set; }
        public int ItemListEntityId { get; set; }
        public ItemListEntity ItemListEntity { get; set; }
        public List<ItemLabelEntity> LabelList{get;set;}
    }
}
