using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Responses.Checklist
{
    public class CheckItemDetailResponse : BaseResponse
    {
        public string Task { get; set; }
        public bool IsChecked { get; set; }
    }
}
