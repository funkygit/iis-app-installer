using DeployBuddy.Models;
using DeployBuddy.Services;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace DeployBuddy.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ServiceConfig> Services { get; set; } = new();
        public ObservableCollection<string> Logs { get; set; } = new();
        public ICommand LoadConfigCommand { get; }
        public ICommand DeployCommand { get; }
        public ICommand HealthCheckCommand { get; }

        private readonly IISManager _iisManager = new();
        private readonly ConfigLoader _configLoader = new();

        public MainViewModel()
        {
            LoadConfigCommand = new RelayCommand(LoadConfig);
            DeployCommand = new RelayCommand(DeployAll);
        }

        private void LoadConfig()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var config = _configLoader.Load(dialog.FileName);
                Services.Clear();
                foreach (var svc in config.Services)
                    Services.Add(svc);

                CheckPortConflicts(config.Services.Select(s => s.Port));
            }
        }

        private void DeployAll()
        {
            foreach (var svc in Services)
            {
                try
                {
                    _iisManager.CreateSite(svc.Name, svc.Path, svc.Port);
                    Log($"Site '{svc.Name}' created on port {svc.Port}.");
                }
                catch (Exception ex)
                {
                    Log($"Error deploying '{svc.Name}': {ex.Message}");
                }
            }
        }

        private void CheckPortConflicts(IEnumerable<int> ports)
        {
            var scanner = new PortScanner();
            var conflicts = scanner.GetConflictingPorts(ports);

            if (conflicts.Any())
            {
                foreach (var port in conflicts)
                    Logger.Log($"⚠️ Port {port} is already in use by another IIS site.");

                MessageBox.Show("Some ports are already in use. Please resolve conflicts before deploying.", "Port Conflict", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Logger.Log("✅ All required ports are available.");
            }
        }

        public async Task<bool> IsSwaggerAvailableAsync(string url)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public void Log(string message)
        {
            Application.Current.Dispatcher.Invoke(() => Logs.Add(message));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
