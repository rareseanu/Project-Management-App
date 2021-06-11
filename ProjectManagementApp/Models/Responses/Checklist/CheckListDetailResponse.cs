using ProjectManagementApp.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Responses.Checklist
{
    public class CheckListDetailResponse : BaseResponse
    {
        public List<CheckItemEntity> CheckItems { get; set; }
        public int ItemEntityId { get; set; }
    }
}
