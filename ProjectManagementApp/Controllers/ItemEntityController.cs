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
    [Route("api/item")]
    public class ItemEntityController : ControllerBase
    {
        private readonly ItemEntityService itemEntityService;

        public ItemEntityController(ItemEntityService itemEntityService)
        {
            this.itemEntityService = itemEntityService;
        }
        [Authorize]
        [HttpGet("/api/item/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await itemEntityService.GetItems(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await itemEntityService.GetItems(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await itemEntityService.GetItems());
        }
        [Authorize]
        [HttpGet("{itemId}")]
        public ObjectResult GetById([FromRoute] int itemId)
        {
            return Ok(itemEntityService.GetItem(itemId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateItem([FromBody] ItemEntity item)
        {
            return Ok(await itemEntityService.Update(item));
        }
        [Authorize]
        [HttpDelete("{itemId}")]
        public async Task<ObjectResult> DeleteItem([FromBody] ItemEntity item)
        {
            return Ok(await itemEntityService.Delete(item));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateItem([FromBody] ItemEntity item)
        {
            return Ok(await itemEntityService.Create(item));
        }
        [Authorize]
        [HttpPost("/api/item/label")]
        public async Task<ObjectResult> AddLabel([FromBody] ItemLabelEntity itemLabel)
        {
            return Ok(await itemEntityService.AddLabel(itemLabel));
        }
        [Authorize]
        [HttpDelete("/api/item/label")]
        public async Task<ObjectResult> DeleteLabel([FromBody] ItemLabelEntity itemLabel)
        {
            return Ok(await itemEntityService.DeleteLabel(itemLabel));
        }
    }
}
