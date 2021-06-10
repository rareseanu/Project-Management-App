using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using Microsoft.AspNetCore.Http;
using ProjectManagementApp.Models.Responses.ItemList;
using ProjectManagementApp.Models.Requests;
using AutoMapper;

namespace ProjectManagementApp.Services
{
    public class CommentaryEntityService : BaseService<CommentaryEntity>
    {
        private readonly CommentaryEntityRepository commentaryEntityRepository;

        public CommentaryEntityService(CommentaryEntityRepository commentaryEntityRepository,
            IHttpContextAccessor contextAccessor) : base(commentaryEntityRepository, contextAccessor)
        {
            this.commentaryEntityRepository = commentaryEntityRepository;
        }
        public async Task<CommentaryEntity> GetCommentary(int itemId)
        {
            return await commentaryEntityRepository.GetAll(p => p.Id == itemId)
                .Include(p => p.ItemEntity)
                .Include(p => p.UserEntity)
                .FirstOrDefaultAsync();
        }
        public async Task<List<CommentaryEntity>> GetCommentaries(int userId)
        {
            return await commentaryEntityRepository.GetAll(p => p.CreatedBy == userId)
                .Include(p => p.ItemEntity)
                .Include(p => p.UserEntity)
                .ToListAsync();
        }
        public async Task<List<CommentaryEntity>> GetCommentaries()
        {
            return await commentaryEntityRepository.GetAll()
                .Include(p => p.ItemEntity)
                .Include(p=>p.UserEntity)
                .ToListAsync();
        }
    }
}
