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
    public class ItemEntityService : BaseService<ItemEntity>
    {
        private readonly ItemEntityRepository itemEntityRepository;
        private readonly ItemLabelEntityRepository itemLabelEntityRepository;

        public ItemEntityService(ItemEntityRepository itemEntityRepository,
            ItemLabelEntityRepository itemLabelEntityRepository,
            IHttpContextAccessor contextAccessor) : base(itemEntityRepository, contextAccessor)
        {
            this.itemEntityRepository = itemEntityRepository;
            this.itemLabelEntityRepository = itemLabelEntityRepository;
        }
        public async Task<ItemEntity> GetItem(int itemId)
        {
            return await itemEntityRepository.GetAll(p => p.Id == itemId)
                .Include(p => p.CheckLists)
                .Include(p => p.CommentaryList)
                .Include(p=>p.ItemListEntity)
                .Include(p=>p.LabelList).FirstOrDefaultAsync();
        }
        public async Task<List<ItemEntity>> GetItems(int userId)
        {
            return await itemEntityRepository.GetAll(p => p.CreatedBy == userId)
                .Include(p => p.CheckLists)
                .Include(p => p.CommentaryList)
                .Include(p => p.ItemListEntity)
                .Include(p => p.LabelList).ToListAsync();
        }
        public async Task<List<ItemEntity>> GetItems()
        {
            return await itemEntityRepository.GetAll()
                .Include(p => p.CheckLists)
                .Include(p => p.CommentaryList)
                .Include(p => p.ItemListEntity)
                .Include(p => p.LabelList).ToListAsync();
        }
        public async Task<ItemLabelEntity> AddLabel(ItemLabelEntity itemLabel)
        {
           return await itemLabelEntityRepository.Create(itemLabel);
        }
        public async Task<ItemLabelEntity> DeleteLabel(ItemLabelEntity itemLabel)
        {
            return await itemLabelEntityRepository.Delete(itemLabel);
        }
    }
}
