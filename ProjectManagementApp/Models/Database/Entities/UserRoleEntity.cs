using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class UserRoleEntity : IdentityUserRole<int>
    {
        [ForeignKey("UserId")] public UserEntity User { get; set; }
        [ForeignKey("RoleId")] public RoleEntity Role { get; set; }
    }
}
