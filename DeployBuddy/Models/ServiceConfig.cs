using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployBuddy.Models
{
    public class ServiceConfig
    {
        public string Name { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string ConnectionString { get; set; }
        public string ApiEndpoint { get; set; }
    }
}
