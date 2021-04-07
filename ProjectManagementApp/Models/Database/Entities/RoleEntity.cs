using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class RoleEntity : IdentityRole<int>
    {
        public List<UserRoleEntity> UserRoles { get; set; }
    }
}
