using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace DeployBuddy.Services
{


    public class PortScanner
    {
        public List<int> GetUsedPorts()
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var listeners = ipProps.GetActiveTcpListeners();
            return listeners.Select(ep => ep.Port).Distinct().ToList();
        }

        public List<int> GetConflictingPorts(IEnumerable<int> requiredPorts)
        {
            var usedPorts = GetUsedPorts();
            return requiredPorts.Where(p => usedPorts.Contains(p)).ToList();
        }
    }

}
