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
    [Route("api/itemlist")]
    public class ItemListEntityController : ControllerBase
    {
        private readonly ItemListEntityService itemListEntityService;

        public ItemListEntityController(ItemListEntityService itemListEntityService)
        {
            this.itemListEntityService = itemListEntityService;
        }
        [Authorize]
        [HttpGet("/api/itemlist/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await itemListEntityService.GetItemLists(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await itemListEntityService.GetItemLists(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await itemListEntityService.GetItemLists());
        }
        [Authorize]
        [HttpGet("{itemListId}")]
        public ObjectResult GetById([FromRoute] int itemListId)
        {
            return Ok(itemListEntityService.GetItemList(itemListId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateItemList([FromBody] ItemListEntity itemList)
        {
            return Ok(await itemListEntityService.Update(itemList));
        }
        [Authorize]
        [HttpDelete("{itemListId}")]
        public async Task<ObjectResult> DeleteItemList([FromBody] ItemListEntity itemList)
        {
            return Ok(await itemListEntityService.Delete(itemList));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateItemList([FromBody] ItemListEntity itemList)
        {
            return Ok(await itemListEntityService.Create(itemList));
        }
    }
}
