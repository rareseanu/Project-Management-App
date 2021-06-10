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
    public class ItemListEntityService : BaseService<ItemListEntity>
    {
        private readonly ItemListEntityRepository itemListEntityRepository;
        private readonly BasePaginationRequest pagination;
        private readonly IMapper mapper;

        public ItemListEntityService(ItemListEntityRepository listEntityRepository,
            IHttpContextAccessor contextAccessor,
            IMapper mapper) : base(listEntityRepository, contextAccessor)
        {
            itemListEntityRepository = listEntityRepository;
            pagination = (BasePaginationRequest)contextAccessor.HttpContext.Items["pagination"];
            this.mapper = mapper;
        }

        public async Task<List<ItemListDetailResponse>> GetItemLists(int userId)
        {
            var result = await Get(p => p.CreatedBy == userId)
                .Skip(pagination.Size * (pagination.Page - 1))
                .Take(pagination.Size)
                .ToListAsync();

            return mapper.Map<List<ItemListDetailResponse>>(result);
        }
        public async Task<ItemListEntity> GetItemList(int itemListId)
        {
            return await itemListEntityRepository.GetAll(p => p.Id == itemListId).Include(p => p.BoardEntity).Include(p => p.ItemEntities).FirstOrDefaultAsync();
        }
        public async Task<List<ItemListEntity>> GetItemLists()
        {
            return await itemListEntityRepository.GetAll().Include(p => p.BoardEntity).Include(p => p.ItemEntities).ToListAsync();
        }
    }
}
