using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployBuddy.Models
{
    public class ConfigInjection
    {
        public string File { get; set; } = string.Empty;
        public List<ConfigVariable> Variables { get; set; } = new();
    }
}
