using Microsoft.Web.Administration;

namespace DeployBuddy.Services
{
    public class PortScanner
    {
        public List<int> GetUsedPorts()
        {
            using var manager = new ServerManager();
            return manager.Sites
                .SelectMany(site => site.Bindings)
                .Select(binding => binding.EndPoint?.Port ?? ParsePort(binding.BindingInformation))
                .Distinct()
                .ToList();
        }

        private int ParsePort(string bindingInfo)
        {
            // Format: IP:Port:HostHeader → e.g., "*:5001:"
            var parts = bindingInfo.Split(':');
            return parts.Length > 1 && int.TryParse(parts[1], out int port) ? port : -1;
        }

        public List<int> GetConflictingPorts(IEnumerable<int> requiredPorts)
        {
            var usedPorts = GetUsedPorts();
            return requiredPorts.Where(p => usedPorts.Contains(p)).ToList();
        }
    }
}
