namespace DeployBuddy.Models
{
    public class ServiceConfig
    {
        public string Name { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public string ConnectionString { get; set; }
        public string ApiEndpoint { get; set; }
        public bool PingSuccessful { get; set; }
        public List<ConfigInjection>? Configuration { get; set; }
    }
}
