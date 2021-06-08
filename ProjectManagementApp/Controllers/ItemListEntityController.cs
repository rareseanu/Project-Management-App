using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Models.Database;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using ProjectManagementApp.Services;

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

        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await itemListEntityService.GetAll());
        }

        [HttpGet("{itemListId}")]
        public async Task<ObjectResult> GetById([FromRoute] int listId)
        {
            return Ok(await itemListEntityService.GetById(listId));
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
