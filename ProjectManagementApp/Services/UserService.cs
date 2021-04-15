using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementApp.Models.Responses;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        private readonly string _tokenKey = @"hfwehdfuhf0jf-23jd9-83u9830ffjn4jffkerfj4j32f9043jfjifjrefjre";

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterRequest userRequest, string role)
        {
            var user = new UserEntity
            {
                UserName = userRequest.Username,
                Email = userRequest.Email
            };

            var result = await userRepository.Register(user, userRequest.Password);

            if (!result.Succeeded)
                return result;

            var roleResult = await userRepository.AddRoleToUser(user, role);

            if (!roleResult.Succeeded)
                return result;

            return result;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            var result = await userRepository.Login(username, password);

            if (!result.Succeeded)
                return null;

            var dbUser = await userRepository.GetUserByUsername(username);

            if (dbUser == null)
                return null;

            dbUser.RefreshToken = GenerateRefreshToken();
            dbUser.RefreshTokenExpires = DateTime.Now.AddMinutes(10d);
            await userRepository.UpdateUser(dbUser);

            return new LoginResponse
            {
                Token = GenerateToken(dbUser),
                RefreshToken = dbUser.RefreshToken,
                User = dbUser,
                Result = result
            };
        }

        public async Task<LoginResponse> RefreshToken(string refreshToken)
        {
            var dbUser = await userRepository.GetUserByRefreshToken(refreshToken);

            if (dbUser?.RefreshTokenExpires == null ||
                dbUser.RefreshTokenExpires < DateTime.Now)
                return null;

            dbUser.RefreshToken = GenerateRefreshToken();
            dbUser.RefreshTokenExpires = DateTime.Now.AddMinutes(10d);
            await userRepository.UpdateUser(dbUser);

            return new LoginResponse
            {
                Token = GenerateToken(dbUser),
                RefreshToken = dbUser.RefreshToken,
                User = dbUser
            };
        }

        public async Task<bool?> RevokeRefreshToken(string refreshToken)
        {
            var dbUser = await userRepository.GetUserByRefreshToken(refreshToken);

            if (dbUser == null)
                return null;

            dbUser.RefreshTokenExpires = DateTime.Now;
            await userRepository.UpdateUser(dbUser);
            return true;
        }

        private string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(user.UserRoles.Select(p => p.Role.Name).Select(p => new Claim(ClaimTypes.Role, p)));

            var token = new JwtSecurityToken(
                "https://trellov2.ro",
                "https://trellov2.ro",
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(5d),
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
