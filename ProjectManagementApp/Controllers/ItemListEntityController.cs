using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ItemListEntityService _itemListEntityService;

        public ListEntityController(ItemListEntityService listEntityService)
        {
            _itemListEntityService = listEntityService;
        }

        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await _itemListEntityService.GetAll());
        }

        [HttpGet("{itemListId}")]
        public async Task<ObjectResult> GetById([FromRoute] int listId)
        {
            return Ok(await _itemListEntityService.GetById(listId));
        }

        [HttpPut]
        public async Task<ObjectResult> UpdateList([FromBody] ItemListEntity list)
        {
            return Ok(await _itemListEntityService.Update(list));
        }

        [HttpDelete("{itemListId}")]
        public async Task<ObjectResult> DeleteList([FromRoute] int listId)
        {
            return Ok(await _itemListEntityService.Delete(listId));
        }

        [HttpPost]
        public async Task<ObjectResult> CreateList([FromBody] ItemListEntity list)
        {
            return Ok(await _itemListEntityService.CreateList(list));
        }
    }
}
