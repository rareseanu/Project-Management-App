using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class BoardUserEntity
    {
        public int Id { get; set; }
        public int UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }
        public int BoardEntityId { get; set; }
        public BoardEntity BoardEntity { get; set; }
        public string BoardRole { get; set; }
    }
}
