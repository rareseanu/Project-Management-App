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
    public class LabelEntityService : BaseService<LabelEntity>
    {
        private readonly LabelEntityRepository labelEntityRepository;

        public LabelEntityService(LabelEntityRepository labelEntityRepository,
            IHttpContextAccessor contextAccessor) : base(labelEntityRepository, contextAccessor)
        {
            this.labelEntityRepository = labelEntityRepository;
        }
        public async Task<LabelEntity> GetLabel(int itemId)
        {
            return await labelEntityRepository.GetAll(p => p.Id == itemId)
                .Include(p => p.BoardEntity)
                .Include(p => p.ItemList)
                .FirstOrDefaultAsync();
        }
        public async Task<List<LabelEntity>> GetLabels(int userId)
        {
            return await labelEntityRepository.GetAll(p => p.CreatedBy == userId)
                .Include(p => p.BoardEntity)
                .Include(p => p.ItemList)
                .ToListAsync();
        }
        public async Task<List<LabelEntity>> GetLabels()
        {
            return await labelEntityRepository.GetAll()
                .Include(p => p.BoardEntity)
                .Include(p => p.ItemList)
                .ToListAsync();
        }
    }
}
