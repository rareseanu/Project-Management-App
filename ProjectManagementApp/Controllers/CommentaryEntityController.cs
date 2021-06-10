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
    [Route("api/commentary")]
    public class CommentaryEntityController : ControllerBase
    {
        private readonly CommentaryEntityService commentaryEntityService;

        public CommentaryEntityController(CommentaryEntityService commentaryEntityService)
        {
            this.commentaryEntityService = commentaryEntityService;
        }
        [Authorize]
        [HttpGet("/api/commentary/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await commentaryEntityService.GetCommentaries(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await commentaryEntityService.GetCommentaries(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await commentaryEntityService.GetCommentaries());
        }
        [Authorize]
        [HttpGet("{commentaryId}")]
        public ObjectResult GetById([FromRoute] int commentaryId)
        {
            return Ok(commentaryEntityService.GetCommentary(commentaryId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateCommentary([FromBody] CommentaryEntity commentary)
        {
            return Ok(await commentaryEntityService.Update(commentary));
        }
        [Authorize]
        [HttpDelete("{commentaryId}")]
        public async Task<ObjectResult> DeleteCommentary([FromBody] CommentaryEntity commentary)
        {
            return Ok(await commentaryEntityService.Delete(commentary));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateCommentary([FromBody] CommentaryEntity commentary)
        {
            return Ok(await commentaryEntityService.Create(commentary));
        }
    }
}
