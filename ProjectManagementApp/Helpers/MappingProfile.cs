using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManagementApp.Models.Database.Entities;
using ProjectManagementApp.Models.Requests;
using ProjectManagementApp.Models.Responses;
using ProjectManagementApp.Models.Responses.Board;
using ProjectManagementApp.Models.Responses.Checklist;
using ProjectManagementApp.Models.Responses.Item;
using ProjectManagementApp.Models.Responses.ItemList;

namespace ProjectManagementApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ItemListEntity, ItemListDetailResponse>();
            CreateMap<BoardEntity, BoardDetailResponse>();
            CreateMap<ItemEntity, ItemDetailResponse>();
            CreateMap<CheckItemEntity, CheckItemDetailResponse>();
            CreateMap<CheckListEntity, CheckListDetailResponse>();
        }
    }
}
