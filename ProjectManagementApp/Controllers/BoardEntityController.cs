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
    [Route("api/board")]
    public class BoardEntityController : ControllerBase
    {
        private readonly BoardEntityService boardEntityService;

        public BoardEntityController(BoardEntityService boardEntityService)
        {
            this.boardEntityService = boardEntityService;
        }
        [Authorize]
        [HttpGet("/api/board/user/{userId}")]
        public async Task<ObjectResult> GetAll([FromRoute] int userId)
        {
            if (userId == User.GetUserId())
            {
                return Ok(await boardEntityService.GetBoards(User.GetUserId()));
            }
            else if (User.GetUserRole() == "Admin")
            {
                return Ok(await boardEntityService.GetBoards(userId));
            }
            return null;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ObjectResult> GetAll()
        {
            return Ok(await boardEntityService.GetBoards());
        }
        [Authorize]
        [HttpGet("{boardId}")]
        public ObjectResult GetById([FromRoute] int boardId)
        {
            return Ok(boardEntityService.GetBoard(boardId));
        }
        [Authorize]
        [HttpPut]
        public async Task<ObjectResult> UpdateBoard([FromBody] BoardEntity board)
        {
            return Ok(await boardEntityService.Update(board));
        }
        [Authorize]
        [HttpDelete("{boardId}")]
        public async Task<ObjectResult> DeleteBoard([FromBody] BoardEntity board)
        {
            return Ok(await boardEntityService.Delete(board));
        }
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> CreateBoard([FromBody] BoardEntity board)
        {
            return Ok(await boardEntityService.Create(board));
        }
        [Authorize]
        [HttpPost("/api/board/users")]
        public async Task<ObjectResult> GivePermissionToUser([FromBody] BoardUserEntity boardUser)
        {
            return Ok(await boardEntityService.GivePermissionToUser(boardUser));
        }
        [Authorize]
        [HttpDelete("/api/board/users")]
        public async Task<ObjectResult> RemovePermissionFromUser([FromBody] BoardUserEntity boardUser)
        {
            return Ok(await boardEntityService.RemovePermissionFromUser(boardUser));
        }
    }
}
