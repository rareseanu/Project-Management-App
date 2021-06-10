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
    public class CheckListEntityService : BaseService<CheckListEntity>
    {
        private readonly CheckListEntityRepository checkListEntityRepository;

        public CheckListEntityService(CheckListEntityRepository checkListEntityRepository,
            IHttpContextAccessor contextAccessor) : base(checkListEntityRepository, contextAccessor)
        {
            this.checkListEntityRepository = checkListEntityRepository;
        }

        public async Task<CheckListEntity> GetCheckList(int checkListId)
        {
           return await checkListEntityRepository.GetAll(p=>p.Id == checkListId).Include(p => p.ItemEntity).Include(p => p.CheckItems).FirstOrDefaultAsync();
        }

        public async Task<List<CheckListEntity>> GetCheckLists(int userId)
        {
            return await checkListEntityRepository.GetAll(p => p.CreatedBy == userId).Include(p => p.ItemEntity).Include(p => p.CheckItems).ToListAsync();
        }

        public async Task<List<CheckListEntity>> GetCheckLists()
        {
            return await checkListEntityRepository.GetAll().Include(p => p.ItemEntity).Include(p => p.CheckItems).ToListAsync();
        }
    }
}
