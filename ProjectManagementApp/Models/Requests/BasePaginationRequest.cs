using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Requests
{
    public class BasePaginationRequest
    {
        public int Size { get; set; }
        public int Page { get; set; } = 1;
        public string OrderBy { get; set; }
        public bool Desc { get; set; }
    }
}
