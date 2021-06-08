using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    [Table("items")]
    public class ItemEntity : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }

    }
}
