using System.IO;

namespace DeployBuddy.Services
{
    public static class ConfigInjector
    {
        public static void InjectConnectionString(string filePath, string connectionString)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Config file not found: {filePath}");

            string content = File.ReadAllText(filePath);
            content = content.Replace("{ConnectionString}", connectionString);
            File.WriteAllText(filePath, content);
        }
    }
}
