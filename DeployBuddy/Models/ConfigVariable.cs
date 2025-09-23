using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployBuddy.Models
{
    public class ConfigVariable
    {
        public string Name { get; set; } = string.Empty;   // e.g. "UserServiceUrl"
        public string Value { get; set; } = string.Empty;  // e.g. "UserService.Port"
    }
}
