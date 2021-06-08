using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Responses
{
    public class BasePaginationResponse<T> where T : class
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public string OrderBy { get; set; }
        public bool Desc { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
