using Microsoft.AspNetCore.Identity;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

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

        public async Task<SignInResult> Login(string username, string password)
        {
            return await userRepository.Login(username, password);
        }
    }
}
