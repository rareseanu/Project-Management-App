using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Helpers;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Models.Responses;
using ProjectManagementApp.Models.Responses.Board;
using ProjectManagementApp.Models.Responses.ItemList;
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
        private readonly BasePaginationRequest pagination;
        private readonly IMapper mapper;

        public BoardEntityService(BoardEntityRepository boardEntityRepository,
            BoardUserEntityRepository boardUserEntityRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper) : base(boardEntityRepository, contextAccessor)
        {
            this.boardEntityRepository = boardEntityRepository;
            this.boardUserEntityRepository = boardUserEntityRepository;
            pagination = (BasePaginationRequest)contextAccessor.HttpContext.Items["pagination"];
            this.mapper = mapper;
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


        public async Task<List<BoardDetailResponse>> GetBoards(int userId)
        {
            var boards = boardEntityRepository.GetAll()
                .Include(p => p.UserList)
                .Where(p => p.UserList.Any(p => p.UserEntityId == userId));

            return mapper.Map<List<BoardDetailResponse>>(await boards.ToListAsync());
        }

        public async Task<BasePaginationResponse<BoardDetailResponse>> GetBoards()
        {
            List<BoardEntity> result;
            if (pagination.Size != 0)
            {
                result = await GetAll()
                    .Include(p => p.UserList)
                    .Skip(pagination.Size * (pagination.Page - 1))
                    .Take(pagination.Size)
                    .ToListAsync();
            }
            else
            {
                result = await GetAll().Include(p => p.UserList).ToListAsync();
            }
            return new BasePaginationResponse<BoardDetailResponse>()
            {
                Data = mapper.Map<List<BoardDetailResponse>>(result),

                TotalCount = result.Count
            };
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
