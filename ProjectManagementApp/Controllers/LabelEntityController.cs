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
    [Route("api/label")]
    public class LabelEntityController : ControllerBase
    {
        private readonly LabelEntityService labelEntityService;

        public LabelEntityController(LabelEntityService labelEntityService)
        {
            this.labelEntityService = labelEntityService;
        }
        [Authorize]
        [HttpGet("/api/label/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await labelEntityService.GetLabels(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await labelEntityService.GetLabels(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await labelEntityService.GetLabels());
        }
        [Authorize]
        [HttpGet("{labelId}")]
        public ObjectResult GetById([FromRoute] int labelId)
        {
            return Ok(labelEntityService.GetLabel(labelId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateLabel([FromBody] LabelEntity label)
        {
            return Ok(await labelEntityService.Update(label));
        }
        [Authorize]
        [HttpDelete("{labelId}")]
        public async Task<ObjectResult> DeleteLabel([FromBody] LabelEntity label)
        {
            return Ok(await labelEntityService.Delete(label));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateLabel([FromBody] LabelEntity label)
        {
            return Ok(await labelEntityService.Create(label));
        }
    }
}
