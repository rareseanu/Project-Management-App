using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Repositories;

namespace ProjectManagementApp.Services
{
    public class ItemListEntityService
    {
        private readonly ItemListEntityRepository _itemListEntityRepository;

        public ItemListEntityService(ItemListEntityRepository listEntityRepository)
        {
            _itemListEntityRepository = listEntityRepository;
        }

        public async Task<ItemListEntity> CreateList(ItemListEntity list)
        {
            ItemListEntity result = await _itemListEntityRepository.Insert(list);
            return result;
        }

        public async Task<List<ItemListEntity>> Search(string text)
        {
            return await _itemListEntityRepository.Search(text);
        }

        public async Task<bool> Delete(int id)
        {
            return await _itemListEntityRepository.Delete(id);
        }

        public async Task<List<ItemListEntity>> GetAll()
        {
            return await _itemListEntityRepository.GetAll();
        }

        public async Task<ItemListEntity> GetById(int id)
        {
            return await _itemListEntityRepository.GetById(id);
        }

        public async Task<ItemListEntity> Update(ItemListEntity list)
        {
            return await _itemListEntityRepository.Update(list);
        }
    }
}
