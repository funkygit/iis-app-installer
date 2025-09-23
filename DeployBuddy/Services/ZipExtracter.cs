using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace DeployBuddy.Services
{
    public class ZipExtractor
    {
        /// <summary>
        /// Extracts an embedded ZIP resource to the target folder.
        /// </summary>
        public void ExtractEmbeddedZip(string resourceName, string targetPath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Embedded resource '{resourceName}' not found.");

            ExtractZipStream(stream, targetPath);
        }

        /// <summary>
        /// Extracts a ZIP file from disk to the target folder.
        /// </summary>
        public void ExtractZipFile(string zipFilePath, string targetPath)
        {
            using var stream = File.OpenRead(zipFilePath);
            ExtractZipStream(stream, targetPath);
        }

        /// <summary>
        /// Core extraction logic.
        /// </summary>
        private void ExtractZipStream(Stream zipStream, string targetPath)
        {
            Directory.CreateDirectory(targetPath);

            using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
            foreach (var entry in archive.Entries)
            {
                string fullPath = Path.Combine(targetPath, entry.FullName);

                if (string.IsNullOrEmpty(entry.Name))
                {
                    Directory.CreateDirectory(fullPath);
                    continue;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                entry.ExtractToFile(fullPath, overwrite: true);
            }
        }
    }

}
