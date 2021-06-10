using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Filters;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using ProjectManagementApp.Services;
using ProjectManagementApp.Helpers;

namespace ProjectManagementApp.Controllers
{
    [ApiController]
    [Route("api/checkitem")]
    public class CheckItemEntityController : ControllerBase
    {
        private readonly CheckItemEntityService checkItemEntityService;

        public CheckItemEntityController(CheckItemEntityService checkItemEntityService)
        {
            this.checkItemEntityService = checkItemEntityService;
        }
        [Authorize]
        [HttpGet("/api/checkitem/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await checkItemEntityService.GetCheckItems(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await checkItemEntityService.GetCheckItems(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await checkItemEntityService.GetCheckItems());
        }
        [Authorize]
        [HttpGet("{checkitemId}")]
        public ObjectResult GetById([FromRoute] int itemId)
        {
            return Ok(checkItemEntityService.GetCheckItem(itemId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateCheckItem([FromBody] CheckItemEntity checkitem)
        {
            return Ok(await checkItemEntityService.Update(checkitem));
        }
        [Authorize]
        [HttpDelete("{checkitemId}")]
        public async Task<ObjectResult> DeleteCheckItem([FromBody] CheckItemEntity checkitem)
        {
            return Ok(await checkItemEntityService.Delete(checkitem));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateCheckItem([FromBody] CheckItemEntity checkitem)
        {
            return Ok(await checkItemEntityService.Create(checkitem));
        }
    }
}
