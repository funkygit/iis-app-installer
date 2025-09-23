using System.IO;
using System.Security.AccessControl;

namespace DeployBuddy.Services
{
    public class PermissionSetter
    {
        public void GrantIISAccess(string folderPath)
        {
            var dirInfo = new DirectoryInfo(folderPath);
            var dirSecurity = dirInfo.GetAccessControl();

            dirSecurity.AddAccessRule(new FileSystemAccessRule(
                "IIS_IUSRS",
                FileSystemRights.Modify | FileSystemRights.ReadAndExecute | FileSystemRights.ListDirectory,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow));

            dirInfo.SetAccessControl(dirSecurity);
        }
    }
}
