using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Models.Database.Entities;

namespace ProjectManagementApp.Models.Responses
{
    public class LoginResponse
    {
        public SignInResult Result { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserEntity User { get; set; }
    }
}
