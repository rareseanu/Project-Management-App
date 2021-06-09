using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models
{ 
    public class EmailConfiguration
    {
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public string EmailConfigurationTemplateId { get; set; }
    }
}
