namespace DeployBuddy.Services
{
    using System.IO;

    public static class FolderCopier
    {
        public static void CopyFolder(string sourcePath, string targetPath, bool overwrite = true)
        {
            if (!Directory.Exists(sourcePath))
                throw new DirectoryNotFoundException($"Source folder not found: {sourcePath}");

            Directory.CreateDirectory(targetPath);

            foreach (var file in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(sourcePath, file);
                string targetFile = Path.Combine(targetPath, relativePath);

                Directory.CreateDirectory(Path.GetDirectoryName(targetFile)!);
                File.Copy(file, targetFile, overwrite);
            }
        }
    }

}
