using System.IO;

namespace DeployBuddy.Services
{
    public static class ZipUtils
    {
        public static bool IsZipFile(string path)
        {
            if (!File.Exists(path))
                return false;

            // Check extension first
            if (!path.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                return false;

            // Validate ZIP header (PK\x03\x04)
            byte[] buffer = new byte[4];
            using var stream = File.OpenRead(path);
            if (stream.Read(buffer, 0, 4) < 4)
                return false;

            return buffer[0] == 'P' && buffer[1] == 'K' && buffer[2] == 3 && buffer[3] == 4;
        }
    }

}
