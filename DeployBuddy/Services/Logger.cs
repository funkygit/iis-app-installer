using System.Collections.ObjectModel;
using System.IO;

namespace DeployBuddy.Services
{
    public static class Logger
    {
        private static readonly string LogDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static readonly string LogFile = Path.Combine(LogDir, $"deploy_{DateTime.Now:yyyyMMdd_HHmmss}.log");

        public static ObservableCollection<string> LogEntries { get; } = new();

        static Logger()
        {
            Directory.CreateDirectory(LogDir);
        }

        public static void Log(string message)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {message}";
            File.AppendAllText(LogFile, entry + Environment.NewLine);
            App.Current.Dispatcher.Invoke(() => LogEntries.Add(entry));
        }
    }
}
