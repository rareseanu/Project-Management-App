using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models.Responses
{
    public class ExceptionResponse
    {
        public string Message { get; set; }
        public string Type { get; set; }
        [JsonIgnore] public string Trace { get; set; }
    }
}
