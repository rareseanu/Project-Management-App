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
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }
        public List<UserRoleEntity> UserRoles { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime? ConfirmationCodeExpires { get; set; }
        public List<BoardUserEntity> BoardList { get; set; }
        public List<CommentaryEntity> CommentaryList { get; set; }
    }
}
