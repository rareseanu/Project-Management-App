using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Responses
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
