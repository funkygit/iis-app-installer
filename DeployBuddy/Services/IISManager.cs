using Microsoft.Web.Administration;

namespace DeployBuddy.Services
{
    public class IISManager
    {
        public void CreateSite(string siteName, string physicalPath, int port)
        {
            using (var manager = new ServerManager())
            {
                if (manager.Sites.Any(s => s.Name == siteName))
                    throw new InvalidOperationException($"Site '{siteName}' already exists.");

                if (!manager.ApplicationPools.Any(p => p.Name == siteName))
                {
                    var pool = manager.ApplicationPools.Add(siteName);
                    pool.ManagedRuntimeVersion = "v4.0";
                    pool.ProcessModel.IdentityType = ProcessModelIdentityType.ApplicationPoolIdentity;
                }

                var site = manager.Sites.Add(siteName, "http", $"*:{port}:", physicalPath);
                site.ApplicationDefaults.ApplicationPoolName = "DefaultAppPool";

                manager.CommitChanges();
            }
        }
    }
}
