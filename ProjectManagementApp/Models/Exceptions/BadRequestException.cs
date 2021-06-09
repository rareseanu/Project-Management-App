using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Exceptions
{
    public class BadRequestException : System.Exception
    {
        public BadRequestException(string message = "Bad request") : base(message)
        {

        }
    }
}
