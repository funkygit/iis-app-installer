using DeployBuddy.Models;

namespace DeployBuddy.Services
{
    public class EndpointResolver
    {
        private readonly Dictionary<string, ServiceConfig> _services;

        public EndpointResolver(IEnumerable<ServiceConfig> services)
        {
            _services = services.ToDictionary(s => s.Name);
        }

        public string Resolve(string input, ServiceConfig current)
        {
            string result = input.Replace("{port}", current.Port.ToString());

            foreach (var svc in _services)
            {
                result = result.Replace($"{{{svc.Key}.port}}", svc.Value.Port.ToString());
                result = result.Replace($"{{{svc.Key}.apiEndpoint}}", svc.Value.ApiEndpoint);
            }

            return result;
        }
    }

}
