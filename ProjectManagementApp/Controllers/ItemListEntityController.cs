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
    [Route("api/itemList")]
    public class ListEntityController : ControllerBase
    {
        private readonly ItemListEntityService itemListEntityService;

        public ListEntityController(ItemListEntityService listEntityService)
        {
            itemListEntityService = listEntityService;
        }

        [PaginationResourceFilter]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await itemListEntityService.GetLists(User.GetUserId()));
        }

        [HttpGet("{itemListId}")]
        public ObjectResult GetById([FromRoute] int listId)
        {
            return Ok(itemListEntityService.GetById(listId));
        }

        [HttpPut]
        public async Task<ObjectResult> UpdateList([FromBody] ItemListEntity list)
        {
            return Ok(await itemListEntityService.Update(list));
        }

        [HttpDelete("{itemListId}")]
        public async Task<ObjectResult> DeleteList([FromBody] ItemListEntity list)
        {
            return Ok(await itemListEntityService.Delete(list));
        }

        [HttpPost]
        public async Task<ObjectResult> CreateList([FromBody] ItemListEntity list)
        {
            return Ok(await itemListEntityService.Create(list));
        }
    }
}
