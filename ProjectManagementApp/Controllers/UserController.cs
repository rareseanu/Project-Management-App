using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        // To-Do: Remove role from querry for security purposes.
        [HttpPost("register")]
        public async Task<ObjectResult> Register([FromBody] UserRegisterRequest userRequest,
            [FromQuery] string role)
        {
            return Ok(await userService.RegisterUser(userRequest, role));
        }

        [HttpPost("Login")]
        public async Task<ObjectResult> Login([FromBody] UserLoginRequest userRequest)
        {
            return Ok(await userService.Login(userRequest.Username, userRequest.Password));
        }

        [HttpPut("token/refresh")]
        public async Task<ObjectResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await userService.RefreshToken(refreshTokenRequest.RefreshToken));
        }

        [HttpPut("token/revoke")]
        public async Task<ObjectResult> RevokeToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await userService.RevokeRefreshToken(refreshTokenRequest.RefreshToken));
        }
    }
}
