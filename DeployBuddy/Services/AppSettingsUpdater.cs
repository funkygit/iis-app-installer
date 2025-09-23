using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DeployBuddy.Services
{
    public class AppSettingsUpdater
    {
        public void UpdateAppSettings(string appSettingsPath, string connStr, string apiEndpoint)
        {
            var json = File.ReadAllText(appSettingsPath);
            var root = JsonNode.Parse(json)!;

            root["ConnectionStrings"]?["DefaultConnection"] = connStr;
            root["ApiSettings"]?["Endpoint"] = apiEndpoint;

            File.WriteAllText(appSettingsPath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
