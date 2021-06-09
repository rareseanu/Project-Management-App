using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Exceptions
{
    public class CustomException : System.Exception
    {
        public CustomException(object? value, HttpStatusCode statusCode)
        {
            Value = value;
            Status = statusCode;
        }

        public object? Value { get; set; }
        public HttpStatusCode Status { get; set; }

    }
}
