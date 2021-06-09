using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Exceptions
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message = "NotFound") : base(message)
        {

        }
    }
}
