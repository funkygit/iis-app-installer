using System.IO;

namespace DeployBuddy.Services
{
    public static class PathTypeChecker
    {
        public enum PathType
        {
            File,
            Directory,
            NotFound
        }

        public static PathType GetPathType(string path)
        {
            if (File.Exists(path))
                return PathType.File;

            if (Directory.Exists(path))
                return PathType.Directory;

            return PathType.NotFound;
        }
    }

}
