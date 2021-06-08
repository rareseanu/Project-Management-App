using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Responses.ItemList;

namespace ProjectManagementApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemListEntity, ItemListDetailResponse>();
        }
    }
}
