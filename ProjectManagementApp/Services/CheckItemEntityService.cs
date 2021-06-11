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
using ProjectManagementApp.Models.Responses.Checklist;

namespace ProjectManagementApp.Services
{
    public class CheckItemEntityService : BaseService<CheckItemEntity>
    {
        private readonly CheckItemEntityRepository checkItemEntityRepository;
        private readonly IMapper mapper;

        public CheckItemEntityService(CheckItemEntityRepository checkItemEntityRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper) : base(checkItemEntityRepository, contextAccessor)
        {
            this.checkItemEntityRepository = checkItemEntityRepository;
            this.mapper = mapper;
        }
        public async Task<CheckItemEntity> GetCheckItem(int checkItemId)
        {
            return await checkItemEntityRepository.GetAll(p => p.Id == checkItemId)
                .Include(p => p.CheckList)
                .FirstOrDefaultAsync();
        }
        public async Task<List<CheckListDetailResponse>> GetCheckItems(int userId)
        {
            return mapper.Map<List<CheckListDetailResponse>>(await checkItemEntityRepository.GetAll(p => p.CreatedBy == userId)
                .Include(p => p.CheckList)
                .ToListAsync());
        }
        public async Task<List<CheckListDetailResponse>> GetCheckItems()
        {
            return mapper.Map<List<CheckListDetailResponse>>(await checkItemEntityRepository.GetAll()
                .Include(p => p.CheckList).ToListAsync());
        }
    }
}
