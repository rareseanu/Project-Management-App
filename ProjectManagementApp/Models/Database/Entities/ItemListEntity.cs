using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class ItemListEntity : BaseEntity
    {
        public string Title { get; set; } 
        public List<ItemEntity> ItemEntities { get; set; }
        public int BoardEntityId { get; set; }
        public BoardEntity BoardEntity { get; set; }
    }
}
