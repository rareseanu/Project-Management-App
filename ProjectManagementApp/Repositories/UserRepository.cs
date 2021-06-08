using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly SignInManager<UserEntity> signInManager;
        private readonly RoleManager<RoleEntity> roleManager;
        public UserRepository(UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager,
            SignInManager<UserEntity> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public IQueryable<UserEntity> Get(Expression<Func<UserEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return userManager.Users
                    .Where(predicate);

            return userManager.Users;
        }
        public async Task<UserEntity> GetUserByUsername(string userName)
        {
            return await userManager.Users
                .Where(p => p.UserName == userName)
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
        {
            return await userManager.Users
                .Where(p => p.RefreshToken == refreshToken)
                .Include(p => p.UserRoles)
                .ThenInclude(p => p.Role)
                .FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> Register(UserEntity user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> Login(string username, string password)
        {
            return await signInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task<IdentityResult> AddRoleToUser(UserEntity user, string role)
        {
            return await userManager.AddToRoleAsync(user, role);
        }
        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            await userManager.UpdateAsync(user);
            return user;
        }
    }
}
