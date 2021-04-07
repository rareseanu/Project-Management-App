using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Database.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public bool IsActive { get; set; }
        public bool TermsAccepted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        public List<UserRoleEntity> UserRoles { get; set; }
    }
}
