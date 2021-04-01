using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    [Table("board")]
    public class BoardEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ItemListEntity>? ItemListEntities { get; set; }
        
        public enum VisibilityType
        {
            PUBLIC,
            PRIVATE
        }
        public VisibilityType Visibility { get; set; }
    }
}
