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
    [Route("api/checklist")]
    public class CheckListEntityController : ControllerBase
    {
        private readonly CheckListEntityService checkListEntityService;

        public CheckListEntityController(CheckListEntityService checkListEntityService)
        {
            this.checkListEntityService = checkListEntityService;
        }
        [Authorize]
        [HttpGet("/api/checklist/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await checkListEntityService.GetCheckLists(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await checkListEntityService.GetCheckLists(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await checkListEntityService.GetCheckLists());
        }
        [Authorize]
        [HttpGet("{checkListId}")]
        public ObjectResult GetById([FromRoute] int checklistId)
        {
            return Ok(checkListEntityService.GetCheckList(checklistId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateCheckList([FromBody] CheckListEntity checkListEntity)
        {
            return Ok(await checkListEntityService.Update(checkListEntity));
        }
        [Authorize]
        [HttpDelete("{checkListId}")]
        public async Task<ObjectResult> DeleteCheckList([FromBody] CheckListEntity checkListEntity)
        {
            return Ok(await checkListEntityService.Delete(checkListEntity));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateCheckList([FromBody] CheckListEntity checkListEntity)
        {
            return Ok(await checkListEntityService.Create(checkListEntity));
        }
    }
}
