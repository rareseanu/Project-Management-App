using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Helpers;
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

        [Authorize]
        [HttpGet]
        public async Task<ObjectResult> GetUserDetails()
        {
            return Ok(await userService.GetUserDetails(User.GetUserId()));
        }

        [HttpPost("Register")]
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

        [HttpPut("Token/Refresh")]
        public async Task<ObjectResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await userService.RefreshToken(refreshTokenRequest.RefreshToken));
        }

        [HttpPut("Token/Revoke")]
        public async Task<ObjectResult> RevokeToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            return Ok(await userService.RevokeRefreshToken(refreshTokenRequest.RefreshToken));
        }

        [HttpPost("Register/ResendEmail/{email}")]
        public async Task<ObjectResult> ResendEmail([FromRoute] string email)
        {
            return Ok(await userService.ResendEmailConfirmation(email));
        }

        [HttpPut("Register/ConfirmEmail")]
        public async Task<ObjectResult> ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {
            return Ok(await userService.ConfirmEmail(email, code));
        }
    }
}
