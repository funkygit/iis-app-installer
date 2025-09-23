using DeployBuddy.Models;

namespace DeployBuddy.Services
{
    public class VariableResolver
    {
        private readonly Dictionary<string, ServiceConfig> _serviceMap;

        public VariableResolver(IEnumerable<ServiceConfig> services)
        {
            _serviceMap = services.ToDictionary(s => s.Name, s => s);
        }

        public string Resolve(string propertyPath)
        {
            var parts = propertyPath.Split('.');
            if (!_serviceMap.TryGetValue(parts[0], out var service))
                return propertyPath;

            object? current = service;
            foreach (var prop in parts.Skip(1))
            {
                var pi = current?.GetType().GetProperty(prop);
                current = pi?.GetValue(current);
                if (current == null) break;
            }

            return current?.ToString() ?? propertyPath;
        }

        public string ReplaceNamedVariables(string content, Dictionary<string, string> variableMap)
        {
            foreach (var kvp in variableMap)
            {
                string resolved = Resolve(kvp.Value);
                content = content.Replace($"{{{kvp.Key}}}", resolved);
            }
            return content;
        }
    }

}
