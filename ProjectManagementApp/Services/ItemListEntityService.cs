using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;
using Microsoft.AspNetCore.Http;

namespace ProjectManagementApp.Services
{
    public class ItemListEntityService : BaseService<ItemListEntity>
    {
        private readonly ItemListEntityRepository itemListEntityRepository;

        public ItemListEntityService(ItemListEntityRepository listEntityRepository, IHttpContextAccessor contextAccessor) : base(listEntityRepository, contextAccessor)
        {
           
        }

        public async Task<List<ItemListEntity>> Search(string text)
        {
            return await itemListEntityRepository.Search(text);
        }

        public async Task<ItemListEntity> GetById(int id)
        {
            return await itemListEntityRepository.GetById(id);
        }
    }
}
