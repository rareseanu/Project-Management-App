using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Exceptions;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Models.Responses;
using ProjectManagementApp.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;
        private readonly EmailService emailService;
        private readonly string tokenKey = @"hfwehdfuhf0jf-23jd9-83u9830ffjn4jffkerfj4j32f9043jfjifjrefjre";
        public UserService(UserRepository userRepository, EmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }

        public IQueryable<UserEntity> Get(Expression<Func<UserEntity, bool>> predicate = null)
        {
            return userRepository.Get(predicate);
        }
        public async Task<UserEntity> GetUserDetails(int userId)
        {
            return await Get(p => p.Id == userId)
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .FirstOrDefaultAsync();
        }
        public async Task<IdentityResult> RegisterUser(UserRegisterRequest userRequest, string role)
        {
            var user = new UserEntity
            {
                UserName = userRequest.Username,
                Email = userRequest.Email,
                IsActive = true,
                ConfirmationCode = new Random().Next(0, 1000000).ToString("D6"),
                ConfirmationCodeExpires = DateTime.Now.AddMinutes(2)
            };

            var result = await userRepository.Register(user, userRequest.Password);

            if (!result.Succeeded)
                return result;

            var roleResult = await userRepository.AddRoleToUser(user, role);

            if (!roleResult.Succeeded)
                return result;

            await emailService.SendEmailConfirmation(user.Email, user.ConfirmationCode);
            return result;
        }

        public async Task<bool> ResendEmailConfirmation(string email)
        {
            var user = await Get(p => p.Email == email).FirstOrDefaultAsync();

            if (user == null)
                throw new NotFoundException("User not found");

            if (user.EmailConfirmed)
                throw new BadRequestException("Already Confirmed");

            user.ConfirmationCode = new Random().Next(0, 1000000).ToString("D6");
            user.ConfirmationCodeExpires = DateTime.Now.AddMinutes(2);
            await userRepository.UpdateUser(user);
            return await emailService.SendEmailConfirmation(email, user.ConfirmationCode);
        }

        public async Task<bool> ConfirmEmail(string email, string code)
        {
            var user = await Get(p => p.Email == email).FirstOrDefaultAsync();

            if (user == null)
                throw new NotFoundException("User not found");

            if (user.EmailConfirmed)
                throw new BadRequestException("Already Confirmed");

            if (user.ConfirmationCode != code || user.ConfirmationCodeExpires < DateTime.Now)
                throw new BadRequestException("Invalid Code");

            var result = await userRepository.ConfirmEmail(user);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            var result = await userRepository.Login(username, password);

            if (!result.Succeeded)
                return null;

            var dbUser = await userRepository.GetUserByUsername(username);

            if (dbUser == null)
                return null;

            if (dbUser.EmailConfirmed == false)
                throw new BadRequestException("User not confirmed");

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
                "https://projectmanagement.ro",
                "https://projectmanagement.ro",
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(5d),
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
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
