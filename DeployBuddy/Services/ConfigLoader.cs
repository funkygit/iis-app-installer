namespace DeployBuddy.Services;

using DeployBuddy.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class ConfigModel
{
    public List<ServiceConfig> Services { get; set; } = new();
}

public class ConfigLoader
{
    public ConfigModel Load(string path)
    {
        string json = File.ReadAllText(path);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<ConfigModel>(json, options) ?? new ConfigModel();
    }
}

