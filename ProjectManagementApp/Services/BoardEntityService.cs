using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Helpers;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class BoardEntityService : BaseService<BoardEntity>
    {
        private readonly BoardEntityRepository boardEntityRepository;
        private readonly BoardUserEntityRepository boardUserEntityRepository;

        public BoardEntityService(BoardEntityRepository boardEntityRepository,
            BoardUserEntityRepository boardUserEntityRepository,
            IHttpContextAccessor contextAccessor) : base(boardEntityRepository, contextAccessor)
        {
            this.boardEntityRepository = boardEntityRepository;
            this.boardUserEntityRepository = boardUserEntityRepository;
        }
        public override Task<BoardEntity> Delete(BoardEntity entity, bool commit = true)
        {
            var userPermission = boardUserEntityRepository.Get(p => p.UserEntityId == CurrentUser.GetUserId()).FirstOrDefault()?.BoardRole;
            if (userPermission.Equals("Owner"))
            {
                return base.Delete(entity, commit);
            }
            return null;
        }

        public override Task<BoardEntity> Update(BoardEntity entity, bool commit = true)
        {
            var userPermission = boardUserEntityRepository.Get(p => p.UserEntityId == CurrentUser.GetUserId()).FirstOrDefault()?.BoardRole;
            if (userPermission.Equals("Owner"))
            {
                return base.Update(entity, commit);
            }
            return null;
        }

        public override async Task<BoardEntity> Create(BoardEntity entity, bool commit = true)
        {       
            var board = await base.Create(entity);
            BoardUserEntity boardUserEntity = new BoardUserEntity() { BoardEntityId = board.Id, UserEntityId = CurrentUser.GetUserId(), BoardRole = "Owner"};
            await boardUserEntityRepository.Create(boardUserEntity);
            return board;
        }

        public async Task<BoardEntity> GetBoard(int boardId)
        {
            return await boardEntityRepository.GetAll(p => p.Id == boardId).Include(p => p.UserList).FirstOrDefaultAsync();
        }

        public async Task<List<BoardEntity>> GetBoards(int userId)
        {
            var boards = boardEntityRepository.GetAll()
                .Include(p => p.UserList)
                .Where(p => p.UserList.Any(p => p.UserEntityId == userId));
            return await boards.ToListAsync();
        }

        public async Task<List<BoardEntity>> GetBoards()
        {
            var boards = boardEntityRepository.GetAll()
                .Include(p => p.UserList);
            return await boards.ToListAsync();
        }
        public async Task<BoardUserEntity> GivePermissionToUser(BoardUserEntity boardUser)
        {
            return await boardUserEntityRepository.Create(boardUser);
        }
        public async Task<BoardUserEntity> RemovePermissionFromUser(BoardUserEntity boardUser)
        {
            return await boardUserEntityRepository.Delete(boardUser);
        }
    }
}
