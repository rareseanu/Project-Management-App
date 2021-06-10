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
    public class CheckItemEntityService : BaseService<CheckItemEntity>
    {
        private readonly CheckItemEntityRepository checkItemEntityRepository;

        public CheckItemEntityService(CheckItemEntityRepository checkItemEntityRepository,
            IHttpContextAccessor contextAccessor) : base(checkItemEntityRepository, contextAccessor)
        {
            this.checkItemEntityRepository = checkItemEntityRepository;
        }
        public async Task<CheckItemEntity> GetCheckItem(int checkItemId)
        {
            return await checkItemEntityRepository.GetAll(p => p.Id == checkItemId)
                .Include(p => p.CheckList)
                .FirstOrDefaultAsync();
        }
        public async Task<List<CheckItemEntity>> GetCheckItems(int userId)
        {
            return await checkItemEntityRepository.GetAll(p => p.CreatedBy == userId)
                .Include(p => p.CheckList)
                .ToListAsync();
        }
        public async Task<List<CheckItemEntity>> GetCheckItems()
        {
            return await checkItemEntityRepository.GetAll()
                .Include(p => p.CheckList).ToListAsync();
        }
    }
}
